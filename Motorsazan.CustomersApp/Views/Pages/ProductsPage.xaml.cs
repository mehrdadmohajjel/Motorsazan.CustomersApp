using Acr.UserDialogs;
using Motorsazan.CustomersApp.Shared.Models.Output.Products;
using Motorsazan.CustomersApp.Shared.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Motorsazan.CustomersApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        private long ProductionCategoryId;
        private OutputGetProductCategoryList item = new OutputGetProductCategoryList();
        public ProductsPage()
        {
            FlowDirection = FlowDirection.RightToLeft;
            InitializeComponent();
        }

        private void ProductCategorylistView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var category = e.SelectedItem as OutputGetProductCategoryList;
            ProductionCategoryId = category.ProductionCategoryId;
        }

        private async void ProductCategorylistView_OnRefreshing(object sender, EventArgs e)
        {

            var source = await GetProductCategoryList();
            ProductCategorylistView.ItemsSource = source;
           // ProductCategorylistView.EndRefresh();

        }

        public async Task<ObservableCollection<OutputGetProductCategoryList>> GetProductCategoryList()
        {
            var url = $"{Settings.BaseUrl}/Products/GetProductCategoryList";

            var source = await ApiConnector<ObservableCollection<OutputGetProductCategoryList>>.Post(url);

            var list = new ObservableCollection<OutputGetProductCategoryList>();
            for (int i = 0; i < source.Count; i++)
            {
                var item = new OutputGetProductCategoryList();
                item.FileName = source[i].FileName;
                item.FileStream = source[i].FileStream;
                item.ProductionCategoryId = source[i].ProductionCategoryId;
                item.ProductionCategoryShowName = source[i].ProductionCategoryShowName;
                var fileBytes = Convert.FromBase64String(source[i].FileStream);
                var stream1 = new MemoryStream(fileBytes);
                item.File = ImageSource.FromStream(() => new MemoryStream(stream1.ToArray()));
                list.Add(item);
            }

            return list;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading(" در حال بارگذاری اطلاعات ....", MaskType.Gradient);
            var source = await GetProductCategoryList();
            ProductCategorylistView.ItemsSource = source;
            UserDialogs.Instance.HideLoading();


        }

        private async void ProductCategorylistView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var Tapped = e.Item as OutputGetProductCategoryList;
            await Navigation.PushAsync(new CategoryProducts(Tapped.ProductionCategoryId));

        }

    }
}
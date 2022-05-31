using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Motorsazan.CustomersApp.Shared.Models.Input.CategoryProducts;
using Motorsazan.CustomersApp.Shared.Models.Output.CategoryProducts;
using Motorsazan.CustomersApp.Shared.Utilities;
using Xamarin.Forms;

namespace Motorsazan.CustomersApp.Views.Pages
{
    public partial class CategoryProducts : ContentPage
    {
        private readonly long _productionCategoryId;

        public CategoryProducts(long productionCategoryId)
        {
            FlowDirection = FlowDirection.RightToLeft;
            InitializeComponent();
            _productionCategoryId = productionCategoryId;
        }

        public string WebServerUrl { get; set; } = Settings.WebServerUrl;

        public async Task<ObservableCollection<OutputGetProductListByProductCategoryId>> GetCategoryProductList(
            long categoryId)
        {
            try
            {
                var list = new ObservableCollection<OutputGetProductListByProductCategoryId>();

                var url = $"{Settings.BaseUrl}/CategoryProducts/GetProductListByProductCategoryId";
                var input = new InputGetProductListByProductCategoryId
                {
                    ProductCategoryId = categoryId
                };
                var source =
                    await ApiConnector<ObservableCollection<OutputGetProductListByProductCategoryId>>.Post(url, input);

                for (var i = 0; i < source.Count; i++)
                {
                    var item = new OutputGetProductListByProductCategoryId();
                    item.ProductionId = source[i].ProductionId;
                    item.EngineType = source[i].EngineType;
                    item.ImageName = WebServerUrl + "/Production/Images/" + source[i].ImageName;
                    list.Add(item);
                }

                return list;
            }
            catch (Exception ex)
            {
                await DisplayAlert("", ex.Message, "OK");
                return null;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading(" در حال بارگذاری اطلاعات ....", MaskType.Gradient);

            var source = await GetCategoryProductList(_productionCategoryId);
            CategoryProductslistView.ItemsSource = source;
            UserDialogs.Instance.HideLoading();
        }

        private void CategoryProductslistView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var category = e.SelectedItem as OutputGetProductListByProductCategoryId;
        }

        private async void CategoryProductslistView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var Tapped = e.Item as OutputGetProductListByProductCategoryId;
            await Navigation.PushAsync(new ProductDetail(Tapped.ProductionId));
        }

        private async void CategoryProductslistView_Refreshing(object sender, EventArgs e)
        {
            var source = await GetCategoryProductList(_productionCategoryId);
            CategoryProductslistView.ItemsSource = source;
            CategoryProductslistView.EndRefresh();
        }
    }
}
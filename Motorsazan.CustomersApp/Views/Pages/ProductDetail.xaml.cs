using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Motorsazan.CustomersApp.Shared.Models.Input.ProductDetail;
using Motorsazan.CustomersApp.Shared.Models.Output.ProductDetail;
using Motorsazan.CustomersApp.Shared.Utilities;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Motorsazan.CustomersApp.Views.Pages
{
    public partial class ProductDetail : ContentPage
    {
        private readonly long _productionId;
        private string _catalog;
        private string _fileName;

        public ProductDetail(long productionId)
        {
            FlowDirection = FlowDirection.RightToLeft;
            InitializeComponent();
            _productionId = productionId;
        }

        public string WebServerUrl { get; set; } = Settings.WebServerUrl;


        public async Task<OutputGetProductDetailListByProductId[]> GetProductDetailListByProductId(long ProductionId)
        {
            var url = $"{Settings.BaseUrl}/ProductDetail/GetProductDetailListByProductId";
            var input = new InputGetProductDetailListByProductId
            {
                ProductionId = ProductionId
            };
            var source = await ApiConnector<OutputGetProductDetailListByProductId[]>.Post(url, input);

            return source;
        }

        protected override async void OnAppearing()
        {
            UserDialogs.Instance.ShowLoading(" در حال بارگذاری اطلاعات ....", MaskType.Gradient);
            base.OnAppearing();
            var source = await GetProductDetailListByProductId(_productionId);
            foreach (var item in source)
            {
                ProductImage.Source = WebServerUrl + "/Production/Images/" +
                                      item.ImageName;
                engineTypeLBL.Text = item.EngineType;
                numberOfCylinderLBL.Text = item.NumberOfCylinder.ToString();
                maxPowerLBL.Text = string.IsNullOrEmpty(item.MaxPower) ? "" : item.MaxPower;
                maxTorqueLBL.Text = string.IsNullOrEmpty(item.MaxTorque) ? "" : item.MaxTorque;
                approximateWeightLBL.Text = item.ApproximateWeight.ToString();
                lengthLBL.Text = item.Length.ToString();
                widthLBL.Text = item.Width.ToString();
                heightLBL.Text = item.Height.ToString();
                _fileName = item.CatalogName;
            }

            UserDialogs.Instance.HideLoading();
        }

        private async void DownloadBtn_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_fileName))
            {
                //await Navigation.PushAsync(new PdfViewer(_productionId));
                var url = WebServerUrl + "/Production/Catalog/" + _fileName;
                var uri = new Uri(url);
                Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            else
            {
                await DisplayAlert("خطا", "متاسفانه فایلی وجود ندارد", "متوجه شدم");
            }
        }
    }
}
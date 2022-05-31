using System;
using System.IO;
using System.Threading.Tasks;
using Motorsazan.CustomersApp.Shared.Models.Input.ProductDetail;
using Motorsazan.CustomersApp.Shared.Models.Output.ProductDetail;
using Motorsazan.CustomersApp.Shared.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Motorsazan.CustomersApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PdfViewer : ContentPage
    {
        private string _catalog;
        private MemoryStream _fileMemoryStream;
        private string _fileName;
        private readonly long _productionId;

        public PdfViewer(long productionId)
        {
            InitializeComponent();
            _productionId = productionId;
        }

        public async void PopulatePdf(long productionId)
        {
            StreamWriter sw = null;
            StreamReader sr = null;

            try
            {
                var source = await GetProductDetailListByProductId(_productionId);
                InputPdfViewer.productionId = _productionId;
                foreach (var item in source) _fileName = item.CatalogName;
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MotorSazan");
                var filePath = Path.Combine(path, _fileName);
                var Base64Stream = Convert.FromBase64String(_catalog);


                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                var file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                InputPdfViewer.FilesArray = file;
                using (var ms = new MemoryStream(Base64Stream))
                {
                    sw = new StreamWriter(ms);
                    sr = new StreamReader(ms);
                    InputPdfViewer.MemoryArray = ms;
                    ms.WriteTo(file);
                    pdfViewerControl.LoadDocument(ms);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("خطا", ex.Message, "متوجه شدم");
            }
            finally
            {
                if (sw != null) sw.Dispose();
                if (sr != null) sr.Dispose();
            }
        }


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
            base.OnAppearing();
            PopulatePdf(_productionId);
        }

        private void printButton_Clicked(object sender, EventArgs e)
        {
        }

        private void printButton_Clicked_1(object sender, EventArgs e)
        {
        }

        public interface ISave
        {
            string Save(MemoryStream _fileMemoryStream);
        }

        public interface IAlertView
        {
            void Show(string message);
        }
    }
}
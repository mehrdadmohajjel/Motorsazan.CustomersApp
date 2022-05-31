using Motorsazan.CustomersApp.Shared.Models.Input.GuaranteeValidation;
using Motorsazan.CustomersApp.Shared.Models.Output.GuaranteeValidation;
using Motorsazan.CustomersApp.Shared.Utilities;
using Motorsazan.CustomersApp.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.XForms.Buttons;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Acr.UserDialogs;
using System.Diagnostics;

namespace Motorsazan.CustomersApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GuaranteeValidationPage : ContentPage
    {
        private int BarcodeTypeValue;

        public GuaranteeValidationPage()
        {
            InitializeComponent();
            FlowDirection = FlowDirection.RightToLeft;

        }

        private async void BarcodeBTN_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new ValidateViaBarcode());

        }

        public async Task<OutputGetProductValidationByProductSerialAndGuaranteeSerial> GetProductValidationByProductSerialAndGuaranteeSerial(InputGetProductValidationByProductSerialAndGuaranteeSerial input)
        {
            var url = $"{Settings.BaseUrl}/GuaranteeValidation/GetProductValidationByProductSerialAndGuaranteeSerial";

            var result = await ApiConnector<OutputGetProductValidationByProductSerialAndGuaranteeSerial>.Post(url, input);
            return result;
        }

        private void ShowSerialBarcodeBTN_Clicked(object sender, EventArgs e)
        {

            BarcodeTypeValue = Convert.ToInt32(GuaranteeBarcodeType.ProductSerial.GetHashCode());
            popupLayout.Show();
        }

        private async void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
                options.PossibleFormats = new List<ZXing.BarcodeFormat>() {
            ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13 ,ZXing.BarcodeFormat.UPC_A,ZXing.BarcodeFormat.MSI,ZXing.BarcodeFormat.IMB,ZXing.BarcodeFormat.AZTEC,ZXing.BarcodeFormat.CODE_39,ZXing.BarcodeFormat.CODE_93
             };
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                Action action = () =>
                                   {
                                       var input = result.Text;
                                       if (BarcodeTypeValue == Convert.ToInt32(GuaranteeBarcodeType.ProductSerial.GetHashCode()))
                                       {
                                           ProductSerialTXT.Text = "";
                                           ProductSerialTXT.Text = input;
                                           popupLayout.IsOpen = false;
                                       }
                                       else
                                       {
                                           GuaranteeSerialTXT.Text = "";
                                           GuaranteeSerialTXT.Text = input;
                                           popupLayout.IsOpen = false;
                                       }

                                   };
                Device.BeginInvokeOnMainThread(action);

            }
            catch (Exception ex)
            {
                sw.Stop();
                throw new Exception(ex.GetBaseException().ToString());

            }
        }

        private void ShowGuaranteeSerialBTN_Clicked(object sender, EventArgs e)
        {
            BarcodeTypeValue = Convert.ToInt32(GuaranteeBarcodeType.GuaranteeSerial.GetHashCode());
            popupLayout.Show();

        }

        private async void validateButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(GuaranteeSerialTXT.Text))
            {
                await DisplayAlert("خطا", "شماره گارانتی خالی است", "متوجه شدم");
            }
            else if (string.IsNullOrEmpty(ProductSerialTXT.Text))
            {
                await DisplayAlert("خطا", "شماره سریال خالی است", "متوجه شدم");

            }
            else
            {
                UserDialogs.Instance.ShowLoading(" در حال بارگذاری ....", MaskType.Gradient);

                InputGetProductValidationByProductSerialAndGuaranteeSerial input = new InputGetProductValidationByProductSerialAndGuaranteeSerial
                {
                    ProductSerial = ProductSerialTXT.Text,
                    GuaranteeSerial = GuaranteeSerialTXT.Text
                };
                var result = await GetProductValidationByProductSerialAndGuaranteeSerial(input);
                if (string.IsNullOrEmpty(result.Result))
                {
                    UserDialogs.Instance.HideLoading();
                    await Navigation.PushAsync(new GuaranteeResult(result, true));

                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await Navigation.PushAsync(new GuaranteeResult(result, false));

                }
            }
        }


    }
}
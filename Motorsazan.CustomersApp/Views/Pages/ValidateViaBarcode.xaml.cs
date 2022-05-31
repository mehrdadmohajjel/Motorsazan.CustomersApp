using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Motorsazan.CustomersApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidateViaBarcode : ContentPage
    {
        public ValidateViaBarcode()
        {
            InitializeComponent();
        }


    private  void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
                options.PossibleFormats = new List<ZXing.BarcodeFormat>() {
                ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13 ,ZXing.BarcodeFormat.UPC_A,ZXing.BarcodeFormat.MSI,ZXing.BarcodeFormat.IMB,ZXing.BarcodeFormat.AZTEC,ZXing.BarcodeFormat.CODE_39,ZXing.BarcodeFormat.CODE_93
             };
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                //var result1 = await scanner.Scan(options);

                Device.BeginInvokeOnMainThread(() =>
                {
                    resultLBL.Text = result.Text;

                });
            }
            catch (Exception ex)
            {
                sw.Stop();
                throw new Exception(ex.GetBaseException().ToString());

            }

        }
    }
}
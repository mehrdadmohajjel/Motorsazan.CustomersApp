using Acr.UserDialogs;
using Motorsazan.CustomersApp.Shared.Models.Output.GuaranteeValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Motorsazan.CustomersApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GuaranteeResult : ContentPage
    {
        private OutputGetProductValidationByProductSerialAndGuaranteeSerial _Input;
        private bool _IsSuccess=false;
        public GuaranteeResult(OutputGetProductValidationByProductSerialAndGuaranteeSerial input,bool IsSuccess)
        {
            FlowDirection = FlowDirection.RightToLeft;
            InitializeComponent();
            _Input = input;
            _IsSuccess = IsSuccess;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.HideLoading();

            if (_IsSuccess)
            {
                ResultnimationView.Animation= "success.json";
                ResultLBL.Text = "  شناسه گارانتی با سریال محصول مطابقت دارد";
                ResultLBL.TextColor = Color.Green;
                DetailLBL.IsVisible = true;
                serialLayout.IsVisible = true;
                guarantiLayout.IsVisible = true;
                EngineTypeLayout.IsVisible = true;
                productLayout.IsVisible = true;
                CustomerLayout.IsVisible = true;
                ProductSerailLBL.Text = _Input.ProductSerial;
                GuaranteeSerialLBL.Text = _Input.GuaranteeSerial;
                EngineTypeDescriptionLBL.Text = _Input.EngineTypeDescription;
                ProductNameLBL.Text = _Input.ProductName;
                CustomerNameLBL.Text = _Input.CustomerName;

            }
            else
            {
                ResultnimationView.Animation= "error.json";
                ResultLBL.Text = "متاسفانه  شناسه گارانتی با سریال محصول مطابقت ندارد";
                ResultLBL.TextColor = Color.Red;
                DetailLBL.IsVisible = false;

            }

        }
    }
}
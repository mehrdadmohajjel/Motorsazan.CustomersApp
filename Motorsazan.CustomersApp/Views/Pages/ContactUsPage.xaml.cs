using Motorsazan.CustomersApp.Shared.Models.Output.ContactUsPage;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Motorsazan.CustomersApp.Views.Pages
{
    public partial class ContactUsPage : ContentPage
	{
		public ContactUsPage ()
		{
			FlowDirection = FlowDirection.RightToLeft;
			InitializeComponent();
			SaleDetail.ItemsSource = GetSource();
		}

		public List<OutputGetUnitPhoneNumber> GetSource()
		{
			var list = new List<OutputGetUnitPhoneNumber>
			{
				new OutputGetUnitPhoneNumber{Id=1,PhoneNumber="04134245976",ShowPhoneNumber="041-34245976",Title = "واحد فروش"},
				new OutputGetUnitPhoneNumber{Id=2,PhoneNumber="04134242662",ShowPhoneNumber="041-34242662",Title = "مدیریت فروش"},
				new OutputGetUnitPhoneNumber{Id=3,PhoneNumber="0413425664",ShowPhoneNumber="041-3425664",Title = "فکس فروش"},
				new OutputGetUnitPhoneNumber{Id=4,PhoneNumber="04134255860",ShowPhoneNumber="041-34255860",Title = "خدمات پس از فروش و شکایات مشتریان"},
			};
			return list;
		}

        private void MenuCallItem_Clicked_Clicked(object sender, EventArgs e)
        {
			var menuItem = sender as MenuItem;
			var contact = menuItem.CommandParameter as OutputGetUnitPhoneNumber;
			if (string.IsNullOrEmpty(contact.PhoneNumber))
			{
				return;
			}

			Device.OpenUri(new Uri(String.Format("tel:{0}", contact.PhoneNumber)));


		}

		private void Button_Clicked(object sender, EventArgs e)
        {
			var url = "http://www.motorsazan.ir";
			var uri = new Uri(url);
			Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
		}
	}
}
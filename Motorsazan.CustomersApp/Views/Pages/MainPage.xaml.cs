using System;
using Motorsazan.CustomersApp.Shared.Models.DomainModels;
using Motorsazan.CustomersApp.Shared.Models.Output.NotificationSlider;
using Motorsazan.CustomersApp.Shared.Utilities;
using Motorsazan.CustomersApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Motorsazan.CustomersApp.Views.Pages
{
    public partial class MainPage : ContentPage
    {
        private Label msg;
        public OutputGetLastNews MyInput;
        public OutputGetLastNews MylimitedInput;

        public MainPage(OutputGetLastNews input = null)
        {
            FlowDirection = FlowDirection.RightToLeft;
            InitializeComponent();
            var item = new MainPageViewModel();
            MyInput = input;
            HomeListView.ItemsSource = item.MainPageCardItems;
        }

        public string WebServerUrl { get; set; } = Settings.WebServerUrl;

        private async void ListView_MenuItemTapped(object sender, ItemTappedEventArgs e)
        {
            var Tapped = e.Item as MainPageList;
            switch (Tapped.PageName)
            {
                case "GuaranteeValidationPage":
                    await Navigation.PushAsync(new GuaranteeValidationPage());
                    break;
                case "ContactUsPage":
                    await Navigation.PushAsync(new ContactUsPage());
                    break;
                case "ProductsPage":
                    await Navigation.PushAsync(new ProductsPage());
                    break;
                case "AgentsPage":
                    await Navigation.PushAsync(new AgentsPage());
                    break;
            }
        }

        protected override async void OnAppearing()
        {
            var current = Connectivity.NetworkAccess;
            var profiles = Connectivity.ConnectionProfiles;
            if (current == NetworkAccess.None) ConnectivitypopUp.Show();
            base.OnAppearing();


            if (MyInput != null)
            {
                NewsImage.Source = WebServerUrl + "/NotificationSlider/" + MyInput.ImageName;
                NewsTopic.Text = MyInput.Topic;
                NewsText.Text = MyInput.NewsText.Substring(0, 120) + "....";
                NewsScroll.IsVisible = true;
            }

            else
            {
                NewsScroll.IsVisible = false;
            }
            //MylimitedInput = new OutputGetLastNews();
            //MylimitedInput.Topic = MyInput.Topic;
            //MylimitedInput.NewsText = MyInput.NewsText.Substring(0, 40) + "...";

            //Addresspopup.BindingContext = MylimitedInput;
            //Addresspopup.PopupView.ShowHeader = true;
            //Addresspopup.IsOpen = true;

            //    var notification = new NotificationRequest
            //    {
            //        BadgeNumber = 1,
            //        Description = MyInput.NewsText,
            //        Title = MyInput.Topic,
            //        ReturningData = MyInput.Topic,
            //        NotificationId = Convert.ToInt32(MyInput.NotificationSliderId),
            //        Silent = false,
            //        CategoryType = NotificationCategoryType.Reminder,
            //        Schedule = new NotificationRequestSchedule
            //        {
            //            NotifyTime = DateTime.Now.AddSeconds(10),
            //            NotifyRepeatInterval = TimeSpan.FromSeconds(30),
            //            RepeatType = NotificationRepeat.TimeInterval
            //        }
            //    };
            //    await notification.Show();
        }


        private async void SfListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var Tapped = e.ItemData as MainPageList;
            switch (Tapped.PageName)
            {
                case "GuaranteeValidationPage":
                    await Navigation.PushAsync(new GuaranteeValidationPage());
                    break;
                case "ContactUsPage":
                    await Navigation.PushAsync(new ContactUsPage());
                    break;
                case "ProductsPage":
                    await Navigation.PushAsync(new ProductsPage());
                    break;
                case "AgentsPage":
                    await Navigation.PushAsync(new AgentsPage());
                    break;
            }
        }

        private void SfButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LastNews(MyInput.Topic, MyInput.NewsText, MyInput.ImageName));
        }
    }
}
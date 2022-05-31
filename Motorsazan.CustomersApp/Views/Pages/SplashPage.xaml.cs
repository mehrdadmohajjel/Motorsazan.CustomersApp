using System.Threading.Tasks;
using Motorsazan.CustomersApp.Shared.Models.Output.NotificationSlider;
using Motorsazan.CustomersApp.Shared.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Motorsazan.CustomersApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        public OutputGetLastNews input;

        public SplashPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            var source = await GetLastNews();

            foreach (var item in source)
                input = new OutputGetLastNews
                {
                    Topic = item.Topic,
                    NewsText = item.NewsText,
                    NotificationSliderId = item.NotificationSliderId,
                    ImageName = item.ImageName
                };

            var mainPage = new MainPage(input);
            await Task.Delay(100);

            Application.Current.MainPage = new NavigationPage(mainPage);
        }

        public async Task<OutputGetLastNews[]> GetLastNews()
        {
            var url = $"{Settings.BaseUrl}/NotificationSlider/GetLastNews";

            var result = await ApiConnector<OutputGetLastNews[]>.Post(url);


            return result;
        }
    }
}
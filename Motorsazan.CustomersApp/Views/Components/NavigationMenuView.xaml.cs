using System;
using System.Reflection;
using System.Threading.Tasks;
using Motorsazan.CustomersApp.Shared.Models.Output.NotificationSlider;
using Motorsazan.CustomersApp.Shared.Utilities;
using Motorsazan.CustomersApp.Views.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Motorsazan.CustomersApp.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationMenuView : ContentView
    {
        public static readonly BindableProperty ProductionsPageNameProperty =
            BindableProperty.Create(nameof(ProductionsPageName), typeof(string), typeof(NavigationMenuView),
                typeof(ProductsPage).FullName);


        public static readonly BindableProperty AgentsPageNameProperty =
            BindableProperty.Create(nameof(AgentsPageName), typeof(string), typeof(NavigationMenuView),
                typeof(AgentsPage).FullName);


        public static readonly BindableProperty HomePageNameProperty =
            BindableProperty.Create(nameof(HomePageName), typeof(string), typeof(NavigationMenuView),
                typeof(MainPage).FullName);


        public static readonly BindableProperty ValidationPageNameProperty =
            BindableProperty.Create(nameof(ValidationPageName), typeof(string), typeof(NavigationMenuView),
                typeof(GuaranteeValidationPage).FullName);


        public static readonly BindableProperty ContactUsPageNameProperty =
            BindableProperty.Create(nameof(ContactUsPageName), typeof(string), typeof(NavigationMenuView),
                typeof(ContactUsPage).FullName);

        private readonly Assembly assembly;
        public OutputGetLastNews input;


        public NavigationMenuView()
        {
            assembly = Assembly.GetExecutingAssembly();
            InitializeComponent();
        }

        public string ProductionsPageName
        {
            get => (string) GetValue(ProductionsPageNameProperty);
            set => SetValue(ProductionsPageNameProperty, value);
        }

        private Page ProductionsPageInstance
        {
            get
            {
                var type = assembly.GetType(ProductionsPageName, false, true);
                return (Page) Activator.CreateInstance(type);
            }
        }

        public string AgentsPageName
        {
            get => (string) GetValue(AgentsPageNameProperty);
            set => SetValue(AgentsPageNameProperty, value);
        }

        private Page AgentsPageInstance
        {
            get
            {
                var type = assembly.GetType(AgentsPageName, false, true);
                return (Page) Activator.CreateInstance(type);
            }
        }

        public string HomePageName
        {
            get => (string) GetValue(HomePageNameProperty);
            set => SetValue(HomePageNameProperty, value);
        }

        private Page HomePageInstance
        {
            get
            {
                var type = assembly.GetType(HomePageName, false, true);
                return (Page) Activator.CreateInstance(type);
            }
        }

        public string ValidationPageName
        {
            get => (string) GetValue(ValidationPageNameProperty);
            set => SetValue(ValidationPageNameProperty, value);
        }

        private Page ValidationPageInstance
        {
            get
            {
                var type = assembly.GetType(ValidationPageName, false, true);
                return (Page) Activator.CreateInstance(type);
            }
        }

        public string ContactUsPageName
        {
            get => (string) GetValue(ContactUsPageNameProperty);
            set => SetValue(ContactUsPageNameProperty, value);
        }

        private Page ContactUsPageInstance
        {
            get
            {
                var type = assembly.GetType(ContactUsPageName, false, true);
                return (Page) Activator.CreateInstance(type);
            }
        }

        private void ProductsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(ProductionsPageInstance);
        }

        private void AgentsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(AgentsPageInstance);
        }

        private async void HomeBtn_Clicked(object sender, EventArgs e)
        {
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
            await Navigation.PushAsync(new MainPage(input));
            // Application.Current.MainPage = new NavigationPage(mainPage);
        }

        private void ValidationBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(ValidationPageInstance);
        }

        private void ContactUsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(ContactUsPageInstance);
        }

        public async Task<OutputGetLastNews[]> GetLastNews()
        {
            var url = $"{Settings.BaseUrl}/NotificationSlider/GetLastNews";

            var result = await ApiConnector<OutputGetLastNews[]>.Post(url);


            return result;
        }
    }
}
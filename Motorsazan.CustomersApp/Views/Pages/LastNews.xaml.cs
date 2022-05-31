using Motorsazan.CustomersApp.Shared.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Motorsazan.CustomersApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LastNews : ContentPage
    {
        private readonly string _newsText = "";
        private readonly string _newsTopic = "";

        public LastNews(string topic, string newsText, string imageName)
        {
            InitializeComponent();
            _newsText = newsText;
            _newsTopic = topic;
            TitleLbl.Text = _newsTopic;
            NewsTextLbl.Text = _newsText;
            NewsImage.Source = WebServerUrl + "/NotificationSlider/" + imageName;
        }

        public string WebServerUrl { get; set; } = Settings.WebServerUrl;
    }
}
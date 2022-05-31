using Xamarin.Forms;

namespace Motorsazan.CustomersApp.Shared.Models.Output.NotificationSlider
{
    public class OutputGetLastNewsViewModel
    {
        public long NotificationSliderId { get; set; }

        public string Topic { get; set; }

        public string NewsText { get; set; }

        public string FileStream { get; set; }

        public string ImageName { get; set; }

        public ImageSource ImageFile { get; set; }
    }
}
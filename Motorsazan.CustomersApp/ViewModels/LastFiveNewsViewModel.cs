using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Motorsazan.CustomersApp.Shared.Models.Output.NotificationSlider;
using Motorsazan.CustomersApp.Shared.Utilities;
using MvvmHelpers.Commands;
using Xamarin.Forms.Internals;

namespace Motorsazan.CustomersApp.ViewModels
{
    [Preserve(AllMembers = true)]
    public class LastFiveNewsViewModel : BaseViewModel
    {
        public LastFiveNewsViewModel()
        {
            refreshCommand = new AsyncCommand(refresh);
            var url = $"{Settings.BaseUrl}/NotificationSlider/GetLastFiveNews";
            var itemList = new List<OutputGetLastNewsViewModel>();


            var result = ApiConnector<List<OutputGetLastNews>>.Post(url);

            foreach (var t in result.Result)
            {
                var item = new OutputGetLastNewsViewModel();
                //var byteArray = Convert.FromBase64String(t.FileStream);
                // Stream stream = new MemoryStream(byteArray);
                //item.ImageFile = ImageSource.FromStream(() => stream);
                // item.FileStream = t.FileStream;
                item.ImageName = t.ImageName;
                item.NewsText = t.NewsText;
                item.Topic = t.Topic;
                itemList.Add(item);
            }
        }

        public ObservableCollection<OutputGetLastNewsViewModel> itemList { get; set; }

        public AsyncCommand refreshCommand { get; }

        public bool IsBusy { get; set; }

        private async Task refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }
    }
}
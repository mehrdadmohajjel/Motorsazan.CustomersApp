using Motorsazan.CustomersApp.Shared.Models.Input.Agents;
using Motorsazan.CustomersApp.Shared.Models.Output.Agents;
using Motorsazan.CustomersApp.Shared.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using LaavorButtonImage;
using Syncfusion.XForms.EffectsView;

namespace Motorsazan.CustomersApp.Views.Pages
{
    public partial class AgentsPage : ContentPage
    {
        private string myPhone;
        private OutputGetAgentCityNameList item = new OutputGetAgentCityNameList();
        public ObservableCollection<OutputGetAgentCityNameList> AgentList;
        public AgentsPage()
        {
            FlowDirection = FlowDirection.RightToLeft;
            InitializeComponent();
        }


        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var agent = e.SelectedItem as OutputGetAgentListByAgentCityName;
            myPhone = agent.AgentPhone;
        }


        private async void ListView_OnRefreshing(object sender, EventArgs e)
        {
            if (item.AgentCityName != null)
            {
                var input = new InputGetAgentListByAgentCityName();
                input.AgentCityName = item.AgentCityName;

                var source = await GetAgentListByAgentCityName(input);
                listView.ItemsSource = source.Select(s => new OutputGetAgentListByAgentCityName()
                {
                    AgentCityName = s.AgentName,
                    AgentAddress = s.AgentAddress,
                    AgentName = s.AgentName,
                    AgentPhone = s.AgentPhone
                });
               // listView.EndRefresh();
            }

        }



        public async Task<ObservableCollection<OutputGetAgentCityNameList>> GetAgentCityNameList()
        {
            var url = $"{Settings.BaseUrl}/Agents/GetAgentCityNameList";

            var result = await ApiConnector<ObservableCollection<OutputGetAgentCityNameList>>.Post(url);
            return result;
        }


        public async Task<ObservableCollection<OutputGetAgentListByAgentCityName>> GetAgentListByAgentCityName(InputGetAgentListByAgentCityName input)
        {
            var url = $"{Settings.BaseUrl}/Agents/GetAgentListByAgentCityName";

            var result = await ApiConnector<ObservableCollection<OutputGetAgentListByAgentCityName>>.Post(url, input);
            return result;
        }
      
        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contact = menuItem.CommandParameter as OutputGetAgentListByAgentCityName;

            if (string.IsNullOrEmpty(contact.AgentPhone))
            {
                return;
            }

            Device.OpenUri(new Uri(String.Format("tel:{0}", contact.AgentPhone.Replace("-", ""))));

            //var phoneDialer = CrossMessaging.Current.PhoneDialer;
            //if (phoneDialer.CanMakePhoneCall)
            //    phoneDialer.MakePhoneCall(contact.AgentPhone.Replace("-", ""));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            AgentList = await GetAgentCityNameList();
            AgentCityNamePicker.DataSource = AgentList;
        }


        private void OnEffectsViewAnimationCompleted(object sender, EventArgs e)
        {
            var effectsView = sender as SfEffectsView;
           var selectedItem = effectsView.BindingContext as OutputGetAgentListByAgentCityName;
            myPhone = selectedItem.AgentPhone;
           
        }


        private async void AgentCityNamePicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            if (AgentCityNamePicker.SelectedIndex != -1)
            {

                var MyList = new ObservableCollection<OutputGetAgentListByAgentCityName>();
                item = (OutputGetAgentCityNameList)AgentCityNamePicker.SelectedItem;
                var input = new InputGetAgentListByAgentCityName();
                input.AgentCityName = item.AgentCityName;
                selectedAgentName.Text = "لیست نمایندگان شهر" + item.AgentCityName;
                selectedAgentName.IsVisible = true;
                var source = await GetAgentListByAgentCityName(input);
                if (source.Count < 1)
                {
                    await DisplayAlert("توجه", "برای شهر انتخابی شما،نمایندگی ثبت نشده است", "متوجه شدم");
                }
                else
                {
                    listView.ItemsSource = source.Select(s => new OutputGetAgentListByAgentCityName()
                    {
                        AgentCityName = s.AgentName,
                        AgentAddress = s.AgentAddress,
                        AgentName = s.AgentName,
                        AgentPhone = s.AgentPhone
                    });
                }
            }
        }
    }
}
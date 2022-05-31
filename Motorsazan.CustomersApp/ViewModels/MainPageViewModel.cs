using System.Collections.ObjectModel;
using Motorsazan.CustomersApp.Shared.Models.DomainModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Motorsazan.CustomersApp.ViewModels
{
    /// <summary>
    ///     ViewModel for stock overview page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class MainPageViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance for the <see cref="MainPageViewModel" /> class.
        /// </summary>
        public MainPageViewModel()
        {
            mainPageCardItems = new ObservableCollection<MainPageList>
            {
                new MainPageList
                {
                    Name = "بررسی اصالت کالا",
                    IconEmbeddedFileName = "resource://Motorsazan.CustomersApp.Resources.Images.Main.guarantee.svg",
                    PageName = "GuaranteeValidationPage",
                    BackgroundGradientStart = "#f59083",
                    BackgroundGradientEnd = "#fae188"
                },
                new MainPageList
                {
                    Name = "لیست محصولات",
                    IconEmbeddedFileName = "resource://Motorsazan.CustomersApp.Resources.Images.Main.product.svg",
                    PageName = "ProductsPage",
                    BackgroundGradientStart = "#ff7272",
                    BackgroundGradientEnd = "#f650c5"
                },
                new MainPageList
                {
                    Name = "لیست نمایندگان",
                    IconEmbeddedFileName =
                        "resource://Motorsazan.CustomersApp.Resources.Images.Main.search_in_list.svg",
                    PageName = "AgentsPage",
                    BackgroundGradientStart = "#5e7cea",
                    BackgroundGradientEnd = "#1dcce3"
                },
                new MainPageList
                {
                    Name = "تماس با ما",
                    IconEmbeddedFileName = "resource://Motorsazan.CustomersApp.Resources.Images.Main.address.svg",
                    PageName = "ContactUsPage",
                    BackgroundGradientStart = "#255ea6",
                    BackgroundGradientEnd = "#b350d1"
                }
            };

            MenuCommand = new Command(MenuButtonClicked);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Invoked when the menu button is clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void MenuButtonClicked(object obj)
        {
            // Do something
        }

        #endregion

        #region Field

        private ObservableCollection<MainPageList> mainPageCardItems;

        private ObservableCollection<MainPageList> mainPageCareListItems;

        #endregion

        #region Properties

        public ObservableCollection<MainPageList> MainPageCardItems
        {
            get => mainPageCardItems;

            private set => SetProperty(ref mainPageCardItems, value);
        }

        public ObservableCollection<MainPageList> HealthCareListItems
        {
            get => mainPageCareListItems;

            private set => SetProperty(ref mainPageCareListItems, value);
        }

        #endregion

        #region Comments

        /// <summary>
        ///     Gets or sets the command is executed when the menu button is clicked.
        /// </summary>
        public Command MenuCommand { get; set; }

        /// <summary>
        ///     Gets or sets the command is executed when the profile image is clicked.
        /// </summary>
        public Command ProfileSelectedCommand { get; set; }

        #endregion
    }
}
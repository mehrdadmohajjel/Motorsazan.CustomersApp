using Xamarin.Forms;

namespace Motorsazan.CustomersApp.Shared.Models.Output.Products
{
    public class OutputGetProductCategoryList
    {
        public long ProductionCategoryId { get; set; }

        public string ProductionCategoryShowName { get; set; }

        public string FileName { get; set; }

        public string FileStream { get; set; }

        public ImageSource File { get; set; }
    }
}

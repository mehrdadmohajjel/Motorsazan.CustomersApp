using System.Reflection;
using Xamarin.Forms;

namespace Motorsazan.CustomersApp.Extensions
{
    public static class StringExtensions
    {
        public static ImageSource ToImageSource(this string resourceId) =>
            ImageSource.FromResource(resourceId, typeof(StringExtensions).GetTypeInfo().Assembly);
    }
}

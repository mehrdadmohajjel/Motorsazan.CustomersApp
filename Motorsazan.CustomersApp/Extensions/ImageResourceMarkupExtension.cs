using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Motorsazan.CustomersApp.Extensions
{
    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public ImageResourceExtension()
        {
        }

        public object ProvideValue(IServiceProvider serviceProvider) => Source?.ToImageSource();
    }
}

using System.Web.Mvc;

namespace Motorsazan.CustomersApp.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) =>
            filters.Add(new HandleErrorAttribute());
    }
}

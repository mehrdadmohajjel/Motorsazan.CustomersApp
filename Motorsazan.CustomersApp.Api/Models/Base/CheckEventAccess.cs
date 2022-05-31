
namespace Motorsazan.CustomersApp.Api.Models.Base
{
    public class CheckEventAccess
    {
        public Status Status { get; set; }
        public CheckEventAccessParams Params { get; set; }
        public class CheckEventAccessParams
        {
            public FormAccessInfo HasAccessToPage { get; set; }
        }
        public class FormAccessInfo
        {
            public bool HasAccess { get; set; }
        }
    }
}
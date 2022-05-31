
namespace Motorsazan.CustomersApp.Api.Models.Base
{
    public class UserInfo
    {
        public Status Status { get; set; }
        public UserInfoParams Params { get; set; }
    }

    public class UserInfoParams
    {
        public User[] List { get; set; }
    }
}
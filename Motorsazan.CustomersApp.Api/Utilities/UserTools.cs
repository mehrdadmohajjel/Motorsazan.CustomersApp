using AuthService;
using Motorsazan.CustomersApp.Api.Models.Base;
using Motorsazan.CustomersApp.Shared.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Motorsazan.CustomersApp.Api.Utilities
{
    public static class UserTools
    {
        public static int? GetOnlineUser()
        {
            var sessionKey = "OnlineUserId";
            var currentSessionValue = HttpContext.Current.Session[sessionKey];
            return currentSessionValue != null ? Convert.ToInt32(currentSessionValue) : 0;
        }

        public static async Task<int> GetUserIdByJsonWebTokenAsync(string jsonWebToken)
        {
            try
            {
                var input = new WebServiceInputGetEndUserInfoByJsonWebToken
                {
                    Parameters = new WebServiceInputGetEndUserInfoByJsonWebTokenParams
                    {
                        JsonWebToken = jsonWebToken
                    }
                };
                var client = new AppClient();
                var output = await client.GetUserInfoByJsonWebTokenAsync(input);
                client.Close();

                if (output.Status.Code != "G00000")
                {
                    return 0;
                }

                return output.Parameters?.UserID ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        public static User GetUserName(long userId)
        {
            try
            {
                var baseUrl = "http://erp-server:2021/api/GetUserInfo";
                var response = ApiConnector<UserInfo>.Get(baseUrl, $"UserId={userId}");
                var successStatus = "1";

                var isResponseValid = !Equals(response, null) && response.Result.Status.Code == successStatus;

                if (!isResponseValid)
                {
                    return null;
                }

                var userInfo = response.Result.Params.List;
                var isUserInfoValid = userInfo != null && userInfo.Any();

                return isUserInfoValid ? userInfo[0] : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool HasAccessToEvent(int? userId, string formCode, string eventCode, string systemCode)
        {
            try
            {
                var baseUrl = "http://erp-server/api/CheckEventAccess";
                var response = ApiConnector<CheckEventAccess>.Get(baseUrl, $"UserID={userId}&FormCode={formCode}&SystemCode={systemCode}&EventCode={eventCode}");

                var isResponseValid = response.Result.Status.Code == "G00000";
                if (!isResponseValid)
                {
                    return false;
                }

                var result = response.Result.Params.HasAccessToPage;
                return result.HasAccess;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool HasAccessToForm(int? userId, string formCode, string systemCode)
        {
            try
            {
                const string baseUrl = "http://erp-server/api/CheckFormAccess";
                var response = ApiConnector<CheckFormAccess>.Get(baseUrl, $"UserID={userId}&FormCode={formCode}&SystemCode={systemCode}");

                var isResponseValid = !Equals(response, null) && response.Result.Status.Code == "G00000";

                if (!isResponseValid)
                {
                    return false;
                }

                var result = response.Result.Params.HasAccessToPage;
                return result?.HasAccess ?? false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<bool> IsSessionValidByJsonWebToken(string jsonWebToken)
        {
            try
            {
                var input = new WebServiceInputValidateSessionByJsonWebToken
                {
                    Parameters = new WebServiceInputValidateSessionByJsonWebTokenParams
                    {
                        JsonWebToken = jsonWebToken
                    }
                };
                var client = new AppClient();
                var output = await client.ValidateSessionByJsonWebTokenAsync(input);
                client.Close();

                return output.Status.Code == "G00000" && output.Parameters.IsValid;
            }
            catch
            {
                return false;
            }
        }
    }
}
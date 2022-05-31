using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Motorsazan.CustomersApp.Shared.Utilities
{
    public class ApiConnector<T>
    {
        public static async Task<T> Get(string url, string parameters = null, string token = null)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(url);
                var hasToken = !string.IsNullOrEmpty(token);
                if (hasToken)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var responseMessage = await client.GetAsync($"{uri}?{parameters}");
                var result = (T)Activator.CreateInstance(typeof(T), new object[] { });

                if (!responseMessage.IsSuccessStatusCode)
                {
                    return result;
                }

                var responseData = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseData);
            }
        }

        public static async Task<T> Post(string url, object parameters = null, string token = null, int timeOutInSecond = 60 * 10)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(timeOutInSecond);
                

                var hasToken = !string.IsNullOrEmpty(token);
                if (hasToken)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
           
                var uri = new Uri(url);
                client.DefaultRequestHeaders.Add("accept", "application/json");
                var serializeParameters = JsonConvert.SerializeObject(parameters);
                var content = new StringContent(serializeParameters, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(jsonResult);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Error");
                }
                else
                {
                    throw new Exception("Error");
                }
            }
        }
    }
}

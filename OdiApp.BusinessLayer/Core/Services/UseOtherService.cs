using OdiApp.BusinessLayer.Core.Services.Interface;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OdiApp.BusinessLayer.Core.Services
{
    public class UseOtherService : IUseOtherService
    {
        public async Task<dynamic> PostMethod(object obj, string endpoint, string jwtToken)
        {
            dynamic result = "";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "odiDil-tr");
            string jsonRequest = JsonSerializer.Serialize(obj);
            try
            {
                StringContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic dync = JsonSerializer.Deserialize<dynamic>(jsonResponse);
                    result = dync.data;
                    // responseObject nesnesini kullanabilirsiniz.
                }
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }

            return result;
        }

        public async Task<dynamic> OdiPostMethod(object obj, string endpoint, string jwtToken)
        {
            dynamic result = "";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "odiDil-tr");
            string jsonRequest = JsonSerializer.Serialize(obj);

            try
            {
                StringContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<dynamic>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return result;
        }

        public async Task<dynamic> GetMethod(string endpoint, string jwtToken)
        {
            dynamic result = "";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "odiDil-tr");

            try
            {

                HttpResponseMessage response = await httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic dync = JsonSerializer.Deserialize<dynamic>(jsonResponse);
                    result = dync.data;
                    // responseObject nesnesini kullanabilirsiniz.
                }
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }

            return result;
        }
    }
}
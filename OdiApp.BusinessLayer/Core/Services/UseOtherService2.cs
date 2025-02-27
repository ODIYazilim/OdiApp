using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DTOs.SharedDTOs;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OdiApp.BusinessLayer.Core.Services
{
    public class UseOtherService2<T> : IUseOtherService2<T>
    {
        public async Task<T> GetMethod(string endpoint, string jwtToken)
        {
            OdiResponse<T> resp = new OdiResponse<T>();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "odiDil-tr");

            try
            {

                HttpResponseMessage response = await httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    resp = JsonSerializer.Deserialize<OdiResponse<T>>(jsonResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }

            return resp.Data;
        }
        public async Task<List<T>> GetMethodList(string endpoint, string jwtToken)
        {
            OdiResponse<List<T>> resp = new OdiResponse<List<T>>();

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
                    var mydata = dync.data;
                    resp = JsonSerializer.Deserialize<OdiResponse<List<T>>>(jsonResponse);
                }
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }

            return resp.Data;
        }

        public async Task<T> PostMethod(object obj, string endpoint, string jwtToken)
        {
            OdiResponse<T> resp = new OdiResponse<T>();
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
                    resp = JsonSerializer.Deserialize<OdiResponse<T>>(jsonResponse);

                    // responseObject nesnesini kullanabilirsiniz.
                }
            }
            catch (Exception ex)
            {

                string msg = ex.Message;
            }

            return resp.Data;
        }

        public Task<List<T>> PostMethodList(object obj, string endpoint, string jwtToken)
        {
            throw new NotImplementedException();
        }
    }
}
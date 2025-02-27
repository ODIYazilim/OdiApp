using OdiApp.DTOs.SharedDTOs;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OdiApp.BusinessLayer.Core.Services
{
    public class WebApiRequest
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WebApiRequest(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //public async Task<OdiResponse<string>> Request(HttpRequestMessage httpRequest)
        //{
        //    try
        //    {
        //        var httpClient = _httpClientFactory.CreateClient();
        //        var httpResponse = await httpClient.SendAsync(httpRequest);

        //        if (httpResponse.IsSuccessStatusCode)
        //        {
        //            string responseContent = await httpResponse.Content.ReadAsStringAsync();

        //            //var  test = await httpResponse.Content.ReadFromJsonAsync<TResult>();

        //            if (!String.IsNullOrEmpty(responseContent))
        //            {
        //                return OdiResponse<string>.Success("HttpRequest success.", responseContent, 200);
        //            }
        //            else
        //            {
        //                return OdiResponse<string>.Fail("HttpRequest failed. Response content is null or empty.", "", 200);
        //            }
        //        }
        //        else
        //        {
        //            if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        //            {
        //                return OdiResponse<string>.Fail("HttpRequest failed. Status code: " + httpResponse.StatusCode.ToString(), "", 200);
        //            }
        //            else if (httpResponse.StatusCode == System.Net.HttpStatusCode.Forbidden)
        //            {
        //                return OdiResponse<string>.Fail("HttpRequest failed. Status code: " + httpResponse.StatusCode.ToString(), "", 200);
        //            }
        //            else
        //            {
        //                return OdiResponse<string>.Fail("HttpRequest failed. Status code: " + httpResponse.StatusCode.ToString(), "", 200);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return OdiResponse<string>.Fail("HttpRequest failed. Exception. ", "", 200);
        //    }
        //}

        public async Task<OdiResponse<TResult>> Get<TResult>(string endpoint, string authorizationToken)
        {
            return await ApiRequest<TResult, object>(HttpMethod.Get, endpoint, authorizationToken, null, null, null);
        }

        public async Task<OdiResponse<TResult>> Post<TResult, TRequest>(string endpoint, string authorizationToken, TRequest requestModel)
        {
            return await ApiRequest<TResult, TRequest>(HttpMethod.Post, endpoint, authorizationToken, requestModel, null, null);
        }

        public async Task<OdiResponse<TResult>> Post<TResult, TRequest>(string endpoint, string authorizationToken, TRequest requestModel, Dictionary<string, string> customHeaders)
        {
            return await ApiRequest<TResult, TRequest>(HttpMethod.Post, endpoint, authorizationToken, requestModel, customHeaders, null);
        }

        public async Task<OdiResponse<TResult>> Post<TResult, TRequest>(string endpoint, string authorizationToken, TRequest requestModel, string contentType)
        {
            return await ApiRequest<TResult, TRequest>(HttpMethod.Post, endpoint, authorizationToken, requestModel, null, contentType);
        }

        public async Task<OdiResponse<TResult>> OdiLoginRequest<TResult>(string endpoint, string authorizationToken, Dictionary<string, string> requestData)
        {
            return await OdiLoginApiRequest<TResult, Dictionary<string, string>>(HttpMethod.Post, endpoint, authorizationToken, requestData, null, "application/x-www-form-urlencoded");
        }

        public async Task<OdiResponse<TResult>> ApiRequest<TResult, TRequest>(HttpMethod httpMethod, string endpoint, string authorizationToken, TRequest requestModel, Dictionary<string, string> customHeaders, string? contentType)
        {
            try
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage(httpMethod, endpoint);

                httpRequest.Headers.Add("Accept-Language", "odiDil-tr");

                if (httpMethod == HttpMethod.Post)
                {
                    if (requestModel != null)
                    {

                        if (requestModel is Dictionary<string, string> requestData)
                        {
                            var formUrlEncoded = new FormUrlEncodedContent(requestData);
                            httpRequest.Content = formUrlEncoded;
                        }
                        else
                        {
                            StringContent httpContent = new StringContent(JsonSerializer.Serialize(requestModel), Encoding.UTF8, "application/json");
                            httpRequest.Content = httpContent;
                        }
                    }
                    else
                    {

                    }
                }

                //Response type
                if (!string.IsNullOrEmpty(contentType))
                {
                    httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                }
                else
                {
                    httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                if (customHeaders != null)
                {
                    foreach (var customHeader in customHeaders)
                    {
                        httpRequest.Headers.Add(customHeader.Key, customHeader.Value);
                    }
                    if (customHeaders.Any(a => a.Key != "user-agent"))
                    {
                        httpRequest.Headers.Add("user-agent", "Odi Web Api Request Library");
                    }
                }
                else
                {
                    httpRequest.Headers.Add("user-agent", "Odi Web Api Request Library");
                }

                // Header Authorization
                if (!string.IsNullOrEmpty(authorizationToken))
                {
                    httpRequest.Headers.Add("Authorization", "Bearer " + authorizationToken);
                }

                //OdiResponse<string> requestResult = await Request(httpRequest);

                var httpClient = _httpClientFactory.CreateClient();
                var httpResponse = await httpClient.SendAsync(httpRequest);

                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true
                };

                var resultModel = JsonSerializer.Deserialize<OdiResponse<TResult>>(responseContent, options);

                return resultModel;
            }
            catch (Exception ex)
            {
                return OdiResponse<TResult>.Fail("Web api request exception.", ex.Message.ToString(), 400);
            }
        }

        public async Task<OdiResponse<TResult>> OdiLoginApiRequest<TResult, TRequest>(HttpMethod httpMethod, string endpoint, string authorizationToken, TRequest requestModel, Dictionary<string, string> customHeaders, string? contentType)
        {
            try
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage(httpMethod, endpoint);

                httpRequest.Headers.Add("Accept-Language", "odiDil-tr");

                if (httpMethod == HttpMethod.Post)
                {
                    if (requestModel != null)
                    {

                        if (requestModel is Dictionary<string, string> requestData)
                        {
                            var formUrlEncoded = new FormUrlEncodedContent(requestData);
                            httpRequest.Content = formUrlEncoded;
                        }
                        else
                        {
                            StringContent httpContent = new StringContent(JsonSerializer.Serialize(requestModel), Encoding.UTF8, "application/json");
                            httpRequest.Content = httpContent;
                        }
                    }
                    else
                    {

                    }
                }

                //Response type
                if (!string.IsNullOrEmpty(contentType))
                {
                    httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                }
                else
                {
                    httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                if (customHeaders != null)
                {
                    foreach (var customHeader in customHeaders)
                    {
                        httpRequest.Headers.Add(customHeader.Key, customHeader.Value);
                    }
                    if (customHeaders.Any(a => a.Key != "user-agent"))
                    {
                        httpRequest.Headers.Add("user-agent", "Odi Web Api Request Library");
                    }
                }
                else
                {
                    httpRequest.Headers.Add("user-agent", "Odi Web Api Request Library");
                }

                // Header Authorization
                if (!string.IsNullOrEmpty(authorizationToken))
                {
                    httpRequest.Headers.Add("Authorization", "Bearer " + authorizationToken);
                }

                // OdiResponse<string> requestResult = await Request(httpRequest);

                var httpClient = _httpClientFactory.CreateClient();
                var httpResponse = await httpClient.SendAsync(httpRequest);

                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true
                };

                var resultModel = JsonSerializer.Deserialize<TResult>(responseContent, options);

                if (resultModel != null)
                {
                    return OdiResponse<TResult>.Success(".", resultModel, 200);
                }
                else
                {
                    return OdiResponse<TResult>.Fail("Result model is null.", "", 400);
                }
            }
            catch (Exception ex)
            {
                return OdiResponse<TResult>.Fail("Web api request exception.", ex.Message.ToString(), 400);
            }
        }
    }
}
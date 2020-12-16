using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.Helper
{
    public static class HttpRestApiClientHelper
    {
        public static async Task<T> GetAsync<T>(string url, HttpClient client)
        {
            using var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                LoggerHelper.Logger.Debug("GetAsync успешно выполнен");
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            LoggerHelper.Logger.Debug($"GetAsync неуспешный статус {response.StatusCode}");
            throw new HttpRequestException(await response.Content.ReadAsStringAsync(),
                new ArgumentException("GetAsync неуспешный статус", $"{response.StatusCode} : {response.Headers} : {response.RequestMessage}"));
        }

        public static async Task<T> GetAsync<T>(string url, StringContent content, HttpClient client)
        {
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            LoggerHelper.Logger.Debug($"GetAsync<T> контент запроса {await content.ReadAsStringAsync()}");

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Content = content
            };
            using var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                LoggerHelper.Logger.Debug("GetAsync успешно выполнен");
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            LoggerHelper.Logger.Debug($"GetAsync неуспешный статус {response.StatusCode}");
            throw new HttpRequestException(await response.Content.ReadAsStringAsync(),
                new ArgumentException("GetAsync неуспешный статус", $"{response.StatusCode} : {response.Headers} : {response.RequestMessage}"));
        }

        public static async Task<HttpResponseMessage> GetAsync(string url, HttpClient client)
        {
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                LoggerHelper.Logger.Debug("GetAsync успешно выполнен");
                return response;
            }
            LoggerHelper.Logger.Debug($"GetAsync неуспешный статус {response.StatusCode}");
            throw new HttpRequestException(await response.Content.ReadAsStringAsync(),
                new ArgumentException("GetAsync неуспешный статус", $"{response.StatusCode} : {response.Headers} : {response.RequestMessage}"));
        }

        public static async Task<HttpResponseMessage> PostAsync(string url, HttpClient client, StringContent content)
        {
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            LoggerHelper.Logger.Debug($"PostAsync контент запроса {await content.ReadAsStringAsync()}");

            var response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                LoggerHelper.Logger.Debug("PostAsync успешно выполнен");
                return response;
            }

            LoggerHelper.Logger.Debug($"PostAsync неуспешный статус {response.StatusCode}");
            throw new HttpRequestException(await response.Content.ReadAsStringAsync(),
                new ArgumentException("PostAsync неуспешный статус", $"{response.StatusCode} : {response.Headers} : {response.RequestMessage}"));
        }

        public static async Task<HttpResponseMessage> DeleteAsync(string url, HttpClient client, StringContent content)
        {
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            LoggerHelper.Logger.Debug($"DeleteAsync контент запроса {await content.ReadAsStringAsync()}");

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url),
                Content = content
            };

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                LoggerHelper.Logger.Debug("DeleteAsync успешно выполнен");
                return response;
            }

            LoggerHelper.Logger.Debug($"DeleteAsync неуспешный статус {response.StatusCode}");
            throw new HttpRequestException(await response.Content.ReadAsStringAsync(),
                new ArgumentException("DeleteAsync неуспешный статус", $"{response.StatusCode} : {response.Headers} : {response.RequestMessage}"));
        }

        public static async Task<HttpResponseMessage> PutAsync(string url, HttpClient client, StringContent content)
        {
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            LoggerHelper.Logger.Debug($"PutAsync контент запроса {await content.ReadAsStringAsync()}");

            var response = await client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                LoggerHelper.Logger.Debug("PutAsync успешно выполнен");
                return response;
            }

            LoggerHelper.Logger.Debug($"PutAsync неуспешный статус {response.StatusCode}");
            throw new HttpRequestException(await response.Content.ReadAsStringAsync(),
                new ArgumentException("PutAsync неуспешный статус", $"{response.StatusCode} : {response.Headers} : {response.RequestMessage}"));
        }
    }
}

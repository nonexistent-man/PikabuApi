using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PikabuApi.Helpers
{
    public static class NetworkHelper
    {
        private static readonly HttpClient _httpClient;

        static NetworkHelper()
        {
            var handler = new HttpClientHandler();
            handler.MaxConnectionsPerServer = int.MaxValue;
            _httpClient = new HttpClient(handler);
        }

        public static async Task<string> GetHtmlPageSource(string url = "")
        {
            var response = await _httpClient.GetAsync(url);
            string result = null;
            if(response != null && response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }
    }
}

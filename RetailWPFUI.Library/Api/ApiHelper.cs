using RetailWPFUI.Library.Helpers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RetailWPFUI.Library.Api
{
    public class ApiHelper
    {
        private HttpClient _apiClient;
        private readonly IConfigHelper _configHelper;

        public HttpClient ApiClient { get{return _apiClient; } }
        public ApiHelper(IConfigHelper configHelper)
        {
            _configHelper = configHelper;
            InitializeClient();
        }

        private void InitializeClient()
        {
            _apiClient = new HttpClient
            {
                BaseAddress = new Uri(_configHelper.GetBaseAddress())
            };
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
    }

}
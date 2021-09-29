using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RetailWPFUI.Library.Api
{
    public class ApiHelper
    {
        private HttpClient _apiClient;

        public HttpClient ApiClient { get{return _apiClient; } }
        public ApiHelper()
        {
            InitializeClient();
        }

        private void InitializeClient()
        {
            var baseAddress = ConfigurationManager.AppSettings["api"];
            _apiClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
    }

  }

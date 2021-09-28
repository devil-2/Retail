using RetailWPFUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RetailWPFUI.Helpers
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _apiClient;
        private string _baseAddress;

        public ApiHelper()
        {
            InitializeClient();
            _baseAddress = ConfigurationManager.AppSettings["api"];
        }

        private void InitializeClient()
        {
            _apiClient = new HttpClient
            {
                BaseAddress = new Uri(_baseAddress)
            };
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<AuthenticatedUser> Authenticate(string userName, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type","password"),
                new KeyValuePair<string,string>("username",userName),
                new KeyValuePair<string,string>("password",password)
            });

            using (HttpResponseMessage response = await _apiClient.PostAsync("/token", data))
            {
                return !response.IsSuccessStatusCode
                    ? throw new Exception(response.ReasonPhrase)
                    : await response.Content.ReadAsAsync<AuthenticatedUser>();
            }
        }
    }
}

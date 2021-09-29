using RetailWPFUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RetailWPFUI.Library.Api
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _apiClient;
        private ILoggedInUserModel _loggedInUser;

        public ApiHelper(ILoggedInUserModel loggedInUser)
        {
           
            _loggedInUser = loggedInUser;
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

        public async Task Authenticate(string userName, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type","password"),
                new KeyValuePair<string,string>("username",userName),
                new KeyValuePair<string,string>("password",password)
            });

            using (HttpResponseMessage response = await _apiClient.PostAsync("/token", data))
            {
                if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
                var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                await GetLoggedInUserInfo(result.Access_Token);
            }
        }

        private async Task GetLoggedInUserInfo(string token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using (HttpResponseMessage response = await _apiClient.GetAsync("/api/User"))
            {
                if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
         
                _loggedInUser = await response.Content.ReadAsAsync<LoggedInUserModel>();
                _loggedInUser.BearerToken = $"Bearer {token}"; 
            }
        }
    }
}

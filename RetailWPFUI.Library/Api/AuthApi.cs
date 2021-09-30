using RetailWPFUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RetailWPFUI.Library.Api
{

    public class AuthApi : IAuthApi
    {
        private readonly ApiHelper _apiHelper;
        private ILoggedInUserModel _loggedInUser;

        public AuthApi(ILoggedInUserModel loggedInUser , ApiHelper apiHelper)
        {
            _loggedInUser = loggedInUser;
            _apiHelper = apiHelper;
        }
       

        public async Task Authenticate(string userName, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type","password"),
                new KeyValuePair<string,string>("username",userName),
                new KeyValuePair<string,string>("password",password)
            });

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync("/token", data))
            {
                if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
                var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                await GetLoggedInUserInfo(result.Access_Token);
            }
        }

        private async Task GetLoggedInUserInfo(string token)
        {
            _apiHelper.ApiClient.DefaultRequestHeaders.Clear();
            _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
            _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/User"))
            {
                if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
         
                _loggedInUser = await response.Content.ReadAsAsync<LoggedInUserModel>();
                _loggedInUser.BearerToken = $"Bearer {token}"; 
            }
        }
    }

    public class ProductApi : IProductApi
    {
        private readonly ApiHelper _apiHelper;

        public ProductApi(ApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<ProductModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/product"))
            {
                if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);

                return await response.Content.ReadAsAsync<List<ProductModel>>();
            }
        }
    }

    public class SaleApi : ISaleApi
    {
        private readonly ApiHelper _apiHelper;

        public SaleApi(ApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task Post(SaleModel model)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Sale", model))
            {
                if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);
            }
        }
    }

}

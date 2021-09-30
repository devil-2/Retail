using RetailDataManager.Library.Internal.DataAccess;
using RetailDataManager.Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailDataManager.Library.DataAccess
{
    public class ProductData
    {
        public const string ConnectionStringName = "AppConnection";
        public const string LoadAllProducts = "dbo.spLoadProducts";
        public const string LoadOneProducts = "dbo.spLoadProductById";

        public async Task<List<ProductModel>> LoadProduct()
        {
            SqlDataAccess dataAccess = new SqlDataAccess();
           
            var result = (await dataAccess.LoadDataAsync<ProductModel, dynamic>(LoadAllProducts, new { }, ConnectionStringName)).ToList();

            return result;
        }

        public async Task<ProductModel> LoadOneProduct( int id)
        {
            SqlDataAccess dataAccess = new SqlDataAccess();

            var result = (await dataAccess.LoadDataAsync<ProductModel, dynamic>(LoadOneProducts, new { Id = id }, ConnectionStringName)).FirstOrDefault();

            return result;
        }
    }
}

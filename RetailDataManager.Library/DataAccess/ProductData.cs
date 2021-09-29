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
        public const string UserStoredProcedure = "dbo.spLoadProducts";

        public async Task<List<ProductModel>> LoadProduct()
        {
            SqlDataAccess dataAccess = new SqlDataAccess();
           
            var result = (await dataAccess.LoadDataAsync<ProductModel, dynamic>(UserStoredProcedure, new { }, ConnectionStringName)).ToList();

            return result;
        }
    }
}

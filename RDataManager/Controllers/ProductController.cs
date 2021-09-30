using RetailDataManager.Library.DataAccess;
using RetailDataManager.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace RDataManager.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    { 
        [HttpGet]
        public async Task<List<ProductModel>> Get()
        {
            ProductData data = new ProductData();
            return await data.LoadProduct();
        }
     
    }
}
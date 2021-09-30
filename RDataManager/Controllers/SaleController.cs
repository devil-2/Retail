using RetailDataManager.Library.DataAccess;
using RetailDataManager.Library.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace RDataManager.Controllers
{
    [Authorize]
    public class SaleController : ApiController
    {
        [HttpPost]
        public async Task Post(SaleModel saleModel)
        {
            SaleData data = new SaleData();

        }
    }
}
using Microsoft.AspNet.Identity;
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
            string userId = RequestContext.Principal.Identity.GetUserId();
            SaleData data = new SaleData();
            await data.SaveSale(saleModel, userId);

        }
    }
}
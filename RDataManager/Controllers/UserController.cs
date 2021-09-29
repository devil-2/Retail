using Microsoft.AspNet.Identity;
using RetailDataManager.Library.DataAccess;
using RetailDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RDataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
      
        // GET: User/Details/5
        [HttpGet]
        public async Task<UserModel> Get()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();
            return await data.GetUserById(userId);
        }
    }
}

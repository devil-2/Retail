using RetailDataManager.Library.Internal.DataAccess;
using RetailDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDataManager.Library.DataAccess
{
    public class UserData
    {
        public const string ConnectionStringName= "AppConnection";
        public const string UserStoredProcedure= "dbo.spLoadUser";

        public async Task<UserModel> GetUserById(string id)
        {
            SqlDataAccess dataAccess = new SqlDataAccess();
            var p = new { Id = id };
           
            var result= (await dataAccess.LoadDataAsync<UserModel, dynamic>(UserStoredProcedure, p, ConnectionStringName)).FirstOrDefault();

            return result;
        }
    }
}

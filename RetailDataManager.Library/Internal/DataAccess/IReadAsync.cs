using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetailDataManager.Library.Internal.DataAccess
{
    public interface IReadAsync
    {
        Task<List<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters, string connectionStringName);
    }
}
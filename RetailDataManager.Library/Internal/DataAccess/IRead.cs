using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetailDataManager.Library.Internal.DataAccess
{
    public interface IRead
    {
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        Task<List<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters, string connectionStringName);
    }
}
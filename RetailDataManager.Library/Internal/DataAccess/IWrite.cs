using System.Threading.Tasks;

namespace RetailDataManager.Library.Internal.DataAccess
{
    public interface IWrite
    {
        void SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
    }
    public interface IWriteAsync
    {
        Task SaveDataAsync<T>(string storedProcedure, T parameters, string connectionStringName);
    }
}
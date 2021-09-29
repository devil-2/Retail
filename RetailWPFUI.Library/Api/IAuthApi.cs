using System.Threading.Tasks;

namespace RetailWPFUI.Library.Api
{
    public interface IAuthApi
    {
        Task Authenticate(string userName, string password);
    }
}
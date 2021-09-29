using System.Threading.Tasks;

namespace RetailWPFUI.Library.Api
{
    public interface IApiHelper
    {
        Task Authenticate(string userName, string password);
    }
}
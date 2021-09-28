using RetailWPFUI.Models;
using System.Threading.Tasks;

namespace RetailWPFUI.Helpers
{
    public interface IApiHelper
    {
        Task<AuthenticatedUser> Authenticate(string userName, string password);
    }
}
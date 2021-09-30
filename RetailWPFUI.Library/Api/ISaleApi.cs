using RetailWPFUI.Library.Models;
using System.Threading.Tasks;

namespace RetailWPFUI.Library.Api
{
    public interface ISaleApi
    {
        Task Post(SaleModel model);
    }
}
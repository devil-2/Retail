using RetailWPFUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetailWPFUI.Library.Api
{
    public interface IProductApi
    {
        Task<List<ProductModel>> GetAll();
    }
}
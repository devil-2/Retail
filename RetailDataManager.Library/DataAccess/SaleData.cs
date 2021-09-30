using RetailDataManager.Library.Internal.DataAccess;
using RetailDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailDataManager.Library.DataAccess
{
    public class SaleData
    {
        public const string ConnectionStringName = "AppConnection";
        public const string SaleInsert = "dbo.spSaleInsert";
        public const string SaleDetailInsert = "dbo.spSaleDetailInsert";
        public const string LoadSaleId = "dbo.spLoadSaleId";

        public async Task SaveSale(SaleModel saleInfo, string cashierId)
        {
            var productData = await new ProductData().LoadProduct();

            var list = saleInfo.SaleDetails.Select(item => 
                            MapSaleModelToSaleModelDB(item, productData.FirstOrDefault(x => x.Id == item.ProductId)));

            SaleDBModel sale = new SaleDBModel
            {
                CashierId = cashierId,
                SubTotal = list.Sum(x => x.PurchasePrice),
                Tax = list.Sum(x => x.Tax)
            };

            SqlDataAccess dataAccess = new SqlDataAccess();

            await dataAccess.SaveDataAsync(SaleInsert, sale, ConnectionStringName);
        
            sale.Id = (await dataAccess.LoadDataAsync<int, dynamic>(LoadSaleId, 
                new { sale.CashierId, sale.SaleDate }, ConnectionStringName)).FirstOrDefault();
            
            
            foreach (var item in list)
            {
                item.SaleId = sale.Id;
                await dataAccess.SaveDataAsync(SaleDetailInsert, item, ConnectionStringName);
            }
        }

        private SaleDetailDBModel MapSaleModelToSaleModelDB(SaleDetail item, ProductModel productModel)
        {
            if (productModel == null) throw new Exception($"The Product Id of {item.ProductId} could not be found in the database!");
            var result = new SaleDetailDBModel { ProductId = item.ProductId, Quantity = item.ProductQuantity };
            result.PurchasePrice = productModel.RetailPrice * item.ProductQuantity;
            result.Tax = productModel.RetailPrice * item.ProductQuantity * productModel.Tax / 100;
            return result;
        }
    }
}

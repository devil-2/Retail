namespace RetailWPFUI.Library.Models
{
    public class CartItemModel {
        public ProductModel Product { get; set; }
        public int QuantityInCart { get; set; }
        public string DisplayText { get { return $"{Product.ProductName} ({QuantityInCart})"; }  }
        public decimal Tax 
        { 
            get 
            {
                return Product.RetailPrice * QuantityInCart * Product.Tax / 100;
            } 
        }
        public decimal Price
        {
            get
            {
                return Product.RetailPrice * QuantityInCart;
            }
        }
    }
}

using Caliburn.Micro;
using RetailWPFUI.Library.Api;
using RetailWPFUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailWPFUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private BindingList<ProductModel> _products;
        private BindingList<CartItemModel> _carts = new BindingList<CartItemModel>();
        private int _itemQuantity = 1;
        private readonly IProductApi _productApi;
        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public SalesViewModel(IProductApi productApi)
        {
            _productApi = productApi;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var data = await _productApi.GetAll();
            Products = new BindingList<ProductModel>(data);
        }

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set 
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }
  
        public BindingList<CartItemModel> Cart
        {
            get { return _carts; }
            set
            {
                _carts = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set 
            { 
                _itemQuantity = value; 
                NotifyOfPropertyChange(() => ItemQuantity); 
                NotifyOfPropertyChange(() => CanAddToCart); 
            }
        }
     
        public string SubTotal
        {
            get
            {
                return CalculateSubTotal().ToString("C");
            }
        }

        private decimal CalculateSubTotal()
        {
            decimal subtotal = 0;
            foreach (CartItemModel item in Cart)
            {
                subtotal += item.Product.RetailPrice * item.QuantityInCart;
            }

            return subtotal;
        }
      
        public string Tax
        {
            get
            {
                return CalculateTax().ToString("C");
            }
        }
        private decimal CalculateTax()
        {
            decimal tax = 0;
            foreach (CartItemModel item in Cart)
            {
                tax += item.QuantityInCart * item.Product.RetailPrice * item.Product.Tax / 100;
            }

            return tax;
        }
        public string Total
        {
            get
            {
                decimal total = CalculateSubTotal() + CalculateTax();
                return total.ToString("C");
            }
        }

        public bool CanAddToCart
        {
            get { return ItemQuantity > 0 && SelectedProduct?.QuantityInStock > ItemQuantity; }
        }
        public void AddToCart() 
        {
            CartItemModel cartItem = Cart.FirstOrDefault(x => x.Product.Id == SelectedProduct.Id);
            if (cartItem != null) {
                cartItem.QuantityInCart += ItemQuantity;
                Cart.Remove(cartItem);
            }
            else
            {
                cartItem = new CartItemModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };
               
            }
            Cart.Add(cartItem);
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
        }

        public bool CanRemoveFromCart
        {
            get { return true; }
        }
        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
        }

        public bool CanCheckOut
        {
            get { return Cart.Any(); }
        }
        public void CheckOut()
        {

        }


    }
}

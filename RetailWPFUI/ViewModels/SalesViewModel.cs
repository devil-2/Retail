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
        private BindingList<string> _carts;
        private int _itemQuantity;
        private readonly IProductApi _productApi;

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
  
        public BindingList<string> Cart
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
            }
        }
     
        public string SubTotal
        {
            get { return "$0.00"; }
        }
        public string Tax
        {
            get { return "$0.00"; }
        }
        public string Total
        {
            get { return "$0.00"; }
        }

        public bool CanAddToCart
        {
            get { return true; }
        }
        public void AddToCart() { 
        
        }

        public bool CanRemoveFromCart
        {
            get { return true; }
        }
        public void RemoveFromCart()
        {

        }

        public bool CanCheckOut
        {
            get { return true; }
        }
        public void CheckOut()
        {

        }


    }
}

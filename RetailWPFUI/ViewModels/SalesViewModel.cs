using AutoMapper;
using Caliburn.Micro;
using RetailWPFUI.Library.Api;
using RetailWPFUI.Library.Models;
using RetailWPFUI.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RetailWPFUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private BindingList<ProductDisplayModel> _products;
        private BindingList<CartItemDisplayModel> _carts = new BindingList<CartItemDisplayModel>();
        private int _itemQuantity = 1;
        private readonly IProductApi _productApi;
        private readonly ISaleApi _saleApi;
        private readonly IMapper _mapper;
        private ProductDisplayModel _selectedProduct;

        public ProductDisplayModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public SalesViewModel(IProductApi productApi, ISaleApi saleApi, IMapper mapper)
        {
            _productApi = productApi;
            _saleApi = saleApi;
            _mapper = mapper;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var data = await _productApi.GetAll();
            var productlist = _mapper.Map<List<ProductDisplayModel>>(data);
            Products = new BindingList<ProductDisplayModel>(productlist);
        }

        public BindingList<ProductDisplayModel> Products
        {
            get { return _products; }
            set 
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }
  
        public BindingList<CartItemDisplayModel> Cart
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
            return Cart.Sum(item => item.Price);
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
            return Cart.Sum(item => item.Tax);
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
            CartItemDisplayModel cartItem = Cart.FirstOrDefault(x => x.Product.Id == SelectedProduct.Id);
            if (cartItem != null) {
                cartItem.QuantityInCart += ItemQuantity;
            }
            else
            {
                cartItem = new CartItemDisplayModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };
                Cart.Add(cartItem);
            }
            
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
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
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        public bool CanCheckOut
        {
            get { return Cart.Any(); }
        }
        public async Task CheckOut()
        {
            List<SaleDetail> saleItems = Cart.Select(x => new SaleDetail { ProductId = x.Product.Id, ProductQuantity = x.QuantityInCart }).ToList();
            SaleModel saleModel = new SaleModel { SaleDetails = saleItems };
            await  _saleApi.Post(saleModel);
        }


    }
}

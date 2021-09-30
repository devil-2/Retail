using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailWPFUI.Models
{
    public class CartItemDisplayModel : INotifyPropertyChanged
    {
        private int _quantityInCart;

        public ProductDisplayModel Product { get; set; }

        public int QuantityInCart { 
            get => _quantityInCart;
            set 
            {
                _quantityInCart = value;
                OnPropertyChanged(nameof(QuantityInCart));
                OnPropertyChanged(nameof(DisplayText));
                OnPropertyChanged(nameof(Tax));
                OnPropertyChanged(nameof(Price));
            } 
        }
    
        public string DisplayText { get { return $"{Product.ProductName} ({QuantityInCart})"; } }
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ProductDisplayModel : INotifyPropertyChanged
    {
        private int _quantityInCart;

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal RetailPrice { get; set; }
        public int QuantityInStock
        {
            get => _quantityInCart;
            set
            {
                _quantityInCart = value;
                OnPropertyChanged(nameof(QuantityInStock));
            }
        }
        public decimal Tax { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

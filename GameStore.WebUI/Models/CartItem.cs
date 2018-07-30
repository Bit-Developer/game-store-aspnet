using GameStore.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class CartItem
    {
        public CartItem()
        {

        }
        public CartItem(Product item)
        {
            ProductItem = item;
            Quantity = 1;
        }
        public Product ProductItem { get; set; }
        public int Quantity { get; set; }

        public int GetItemId()
        {
            return ProductItem.ProductId;
        }

        public string GetItemName()
        {
            return ProductItem.ProductName;
        }

        public void IncrementItemQuantity()
        {
            Quantity = Quantity + 1;
        }


        public double GetDiscountedPrice()
        {
            return ProductItem.Price * (100 - ProductItem.Discount) / 100;
        }     

        public double GetTotalCost()
        {
            return Quantity * GetDiscountedPrice();
        }
    }
}
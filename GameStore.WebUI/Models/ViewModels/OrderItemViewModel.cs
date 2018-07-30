using GameStore.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class OrderItemViewModel
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string Condition { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public double GetDiscountedPrice()
        {
            return Price * (100 - Discount) / 100;
        }
        public double GetTotalCost()
        {
            return Quantity * GetDiscountedPrice();
        }
    }
}
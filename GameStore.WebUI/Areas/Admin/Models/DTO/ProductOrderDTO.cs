using GameStore.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Areas.Admin.Models.DTO
{
    public class ProductOrderDTO : Product
    {
        public string CategoryName { get; set; }

        public double GetDiscountedPrice()
        {
            return Price * (100 - Discount) / 100;
        }

        public IEnumerable<OrderDTO> Orders { get; set; }
    }
}
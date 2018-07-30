using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Model
{
    public partial class OrderItem
    {
        public OrderItem()
        {

        }
        //public OrderItem(int OrderId, int ProductId, int Quantity)
        //{
        //    this.OrderId = OrderId;
        //    this.ProductId = ProductId;
        //    this.Quantity = Quantity;
        //}
        [Required]
        public int OrderItemId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}

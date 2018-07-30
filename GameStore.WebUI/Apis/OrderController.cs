using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Http;
using GameStore.Domain.Infrastructure;
using GameStore.Domain.Model;
using System.Net.Http;
using System.Net;
using GameStore.WebUI.Areas.Admin.Models.DTO;
using GameStore.WebUI.Areas.Admin.Models;

namespace GameStore.WebUI.Apis
{
    [Authorize(Roles = "Admin")]
    public class OrderController : ApiController
    {
        // GET api/<controller>
        public List<OrderDTO> Get()
        {
            List<OrderDTO> list = new List<OrderDTO>();

            using (GameStoreDBContext context = new GameStoreDBContext())
            {
                var orders = from o in context.Orders
                             join u in context.Users
                               on o.UserId equals u.Id
                          orderby o.OrderId descending
                           select new { o.OrderId, o.UserId, u.UserName, o.FullName, o.Address, o.City, o.State, o.Zip, o.ConfirmationNumber, o.DeliveryDate };
                list = orders.Select(o => new OrderDTO { OrderId = o.OrderId, UserId = o.UserId, UserName = o.UserName, FullName = o.FullName, Address = o.Address, City = o.City, State = o.State, Zip = o.Zip, ConfirmationNumber = o.ConfirmationNumber, DeliveryDate = o.DeliveryDate }).ToList();

            }

            return list;
        }
        [Route("api/Order/GetOrderItems/{id}")]
        public List<OrderItemDTO> GetOrderItems(int id)
        {
            List<OrderItemDTO> list = new List<OrderItemDTO>();

            using (GameStoreDBContext context = new GameStoreDBContext())
            {
                var orderitems = from i in context.OrderItems
                                 join p in context.Products
                                 on i.ProductId equals p.ProductId
                                 join c in context.Categories
                                 on p.CategoryId equals c.CategoryId
                                 where i.OrderId == id
                                 select new { i.OrderItemId, i.OrderId, i.ProductId, p.ProductName, p.CategoryId, c.CategoryName, p.Price, p.Image, p.Condition, p.Discount, i.Quantity };
                list = orderitems.Select(o => new OrderItemDTO { OrderItemId = o.OrderItemId, OrderId = o.OrderId, ProductId = o.ProductId, ProductName = o.ProductName, CategoryId = o.CategoryId, CategoryName = o.CategoryName, Price = o.Price, Image = o.Image, Condition = o.Condition, Discount = o.Discount, Quantity = o.Quantity }).ToList();
            }

            return list;
        }

        // GET: api/Order/GetCount/
        [Route("api/Order/GetCount")]
        public int GetCount()
        {
            using (GameStoreDBContext context = new GameStoreDBContext())
            {
                return context.Orders.Count();
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            using (GameStoreDBContext context = new GameStoreDBContext())
            {
                var order = context.Orders.Find(id);
                if (order == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "No such order [" + id + "].");
                }
                context.Orders.Remove(order);
                context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Okay");
            }
        }
    }
}
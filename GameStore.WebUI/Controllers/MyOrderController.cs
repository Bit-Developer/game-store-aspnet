using GameStore.Domain.Infrastructure;
using GameStore.WebUI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    [Authorize]
    public class MyOrderController : Controller
    {
        // GET: MyOrder
        public ActionResult Index()
        {
            List<OrderViewModel> list = new List<OrderViewModel>();
            try
            {
                String userid = User.Identity.GetUserId();
                using (GameStoreDBContext context = new GameStoreDBContext())
                {
                    var orders = from o in context.Orders
                                 join u in context.Users
                                   on o.UserId equals u.Id
                                 where o.UserId == userid
                                 orderby o.OrderId descending
                                 select new { o.OrderId, o.UserId, u.UserName, o.FullName, o.Address, o.City, o.State, o.Zip, o.ConfirmationNumber, o.DeliveryDate };
                    list = orders.Select(o => new OrderViewModel { OrderId = o.OrderId, UserId = o.UserId, UserName = o.UserName, FullName = o.FullName, Address = o.Address, City = o.City, State = o.State, Zip = o.Zip, ConfirmationNumber = o.ConfirmationNumber, DeliveryDate = o.DeliveryDate }).ToList();

                    foreach (OrderViewModel order in list)
                    {
                        var orderitems = from i in context.OrderItems
                                         join p in context.Products
                                           on i.ProductId equals p.ProductId
                                         join c in context.Categories
                                           on p.CategoryId equals c.CategoryId
                                         where i.OrderId == order.OrderId
                                         select new { i.OrderItemId, i.OrderId, i.ProductId, p.ProductName, p.CategoryId, c.CategoryName, p.Price, p.Image, p.Condition, p.Discount, i.Quantity };
                        order.Items = orderitems.Select(o => new OrderItemViewModel { OrderItemId = o.OrderItemId, OrderId = o.OrderId, ProductId = o.ProductId, ProductName = o.ProductName, CategoryId = o.CategoryId, CategoryName = o.CategoryName, Price = o.Price, Image = o.Image, Condition = o.Condition, Discount = o.Discount, Quantity = o.Quantity }).ToList();
                    }
                    Session["OrderCount"] = orders.Count();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error Occurs:" + ex.Message;
            }

            return View(list);
        }

        public ActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CancelOrder(int id)
        {
            using (GameStoreDBContext context = new GameStoreDBContext())
            {
                var order = context.Orders.Find(id);
                if (order == null)
                {
                    ViewBag.Message = string.Format("No such order [{0}] found.", id);
                }
                else {
                    context.Orders.Remove(order);
                    context.SaveChanges();
                    ViewBag.Message = string.Format("Order [{0}] has been deleted!", id);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
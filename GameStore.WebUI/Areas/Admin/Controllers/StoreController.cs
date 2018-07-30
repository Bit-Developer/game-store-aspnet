using GameStore.Domain.Infrastructure;
using GameStore.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StoreController : Controller
    {
        // GET: Product
        public ActionResult Product()
        {
            List<Category> list = new List<Category>();
            using (GameStoreDBContext context = new GameStoreDBContext())
            {
                list = context.Categories.ToList();
            }

            ViewBag.Categories = list;
            List<Category> alllist = new List<Category>(list);
            alllist.Insert(0, new Category { CategoryId = 0, CategoryName = "Select All" });
            ViewBag.CategoryFilter = alllist;
            return View();
        }

        public ActionResult Category()
        {
            return View();
        }

        public ActionResult Order()
        {
            return View();
        }
    }
}
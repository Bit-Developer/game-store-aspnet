using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClearCache()
        {
            var itemsInCache = System.Web.HttpContext.Current.Cache.GetEnumerator();

            while (itemsInCache.MoveNext())
            {
                System.Web.HttpContext.Current.Cache.Remove(itemsInCache.Key.ToString());

            }
            return View("Index");
        }
    }
}
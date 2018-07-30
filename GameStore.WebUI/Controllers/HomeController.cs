using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    //[OutputCache(CacheProfile = "StaticUser")]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            
            return View();
        }
    }
}
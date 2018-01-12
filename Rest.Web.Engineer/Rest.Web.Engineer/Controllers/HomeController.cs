using System.Web.Mvc;

namespace Rest.Web.Engineer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.test = true;
            return View();
        }
    }
}
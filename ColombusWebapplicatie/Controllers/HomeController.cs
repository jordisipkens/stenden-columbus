using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Android()
        {
            return View();
        }
    }
}
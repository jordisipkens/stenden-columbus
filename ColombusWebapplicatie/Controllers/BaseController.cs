using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class BaseController : Controller
    {

        public ActionResult Error(string errorMessage, ActionResult result)
        {
            TempData["error"] = errorMessage;
            return result;
        }

        public ActionResult ErrorToIndex(string errorMessage)
        {
            return Error(errorMessage, RedirectToAction("Index"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ColombusWebapplicatie.Models;

namespace ColombusWebapplicatie.Controllers
{
    public class BaseController : Controller
    {
        public string apiUrl = "";

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

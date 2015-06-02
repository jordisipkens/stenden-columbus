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

        public ActionResult Error(ActionResult result, params string[] errorMessages)
        {
            TempData["error"] = errorMessages;
            return result;
        }

        public ActionResult ErrorToIndex(params string[] errorMessages)
        {
            return Error(RedirectToAction("Index"), errorMessages);
        }

        public string GetFullName(User user)
        {
            return user.FirstName + " " + user.LastName;
        }
    }
}

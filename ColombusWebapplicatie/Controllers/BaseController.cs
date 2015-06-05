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
        public ActionResult Error(ActionResult result, params string[] errorMessages)
        {
            TempData["error"] = errorMessages;
            return result;
        }

        public ActionResult ErrorToIndex(params string[] errorMessages)
        {
            return Error(RedirectToAction("Index"), errorMessages);
        }

        public ActionResult Message(ActionResult result, params string[] messages)
        {
            TempData["message"] = messages;
            return result;
        }

        public ActionResult MessageToIndex(params string[] messages)
        {
            return Message(RedirectToAction("Index"), messages);
        }
    }
}

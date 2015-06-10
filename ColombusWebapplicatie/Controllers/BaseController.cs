using ColombusWebapplicatie.Models;
using System.Web.Mvc;

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

        public User GetCurrentUser()
        {
            var user = Session["User"];
            if(user != null && user.GetType() == typeof(User)) {
                return user as User;
            }
            return null;
        }
    }
}
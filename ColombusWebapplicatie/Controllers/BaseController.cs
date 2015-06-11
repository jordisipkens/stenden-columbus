using ColombusWebapplicatie.Models;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Displays an error at the top of the page.
        /// </summary>
        /// <param name="result">Action to go to</param>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        public ActionResult Error(ActionResult result, params string[] errorMessages)
        {
            TempData["error"] = errorMessages;
            return result;
        }

        /// <summary>
        /// Displays an error at the top of the page and returns to the Index of the current Controller.
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <returns></returns>
        public ActionResult ErrorToIndex(params string[] errorMessages)
        {
            return Error(RedirectToAction("Index"), errorMessages);
        }

        /// <summary>
        /// Displays an message at the top of the page.
        /// </summary>
        /// <param name="result">Action to go to</param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public ActionResult Message(ActionResult result, params string[] messages)
        {
            TempData["message"] = messages;
            return result;
        }

        /// <summary>
        /// Displays an message at the top of the page and returns to the Index of the current Controller.
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public ActionResult MessageToIndex(params string[] messages)
        {
            return Message(RedirectToAction("Index"), messages);
        }

        /// <summary>
        /// Gets the current LoggedIn user.
        /// </summary>
        /// <returns></returns>
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
using ColombusWebapplicatie.Classes.Http;
using ColombusWebapplicatie.Models;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class AccountController : BaseController
    {
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        public RedirectToRouteResult Login(User user)
        {
            LoginResponse response = HTTPManager.LoginRequest(user);
            if(response != null) {
                CookieManager.CreateCookie(Response, "Token", response.Token);
                Session["LoggedIn"] = true;
                Session["User"] = response.User;
                Session.Timeout = 180;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult RegisterUser(User user)
        {
            if(ModelState.IsValid) {
                User addedUser = HTTPManager.WebserviceGetRequest<User>("User", Request);
                return RedirectToAction("Index", "Home");
            }
            else {
                return View("Register", user);
            }
        }

        public RedirectToRouteResult Logout()
        {
            Session["LoggedIn"] = false;
            return RedirectToAction("Index", "Home");
        }
    }
}
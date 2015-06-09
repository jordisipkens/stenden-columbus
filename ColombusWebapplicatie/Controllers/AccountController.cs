using ColombusWebapplicatie.Classes;
using ColombusWebapplicatie.Classes.Http;
using ColombusWebapplicatie.Models;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(User user)
        {
            if(user.Username != null || user.Password != null) {
                LoginResponse response = HttpManager.LoginRequest(user);
                if(response != null) {
                    CookieManager.CreateCookie(Response, "Token", response.Token);
                    Session["LoggedIn"] = true;
                    Session["User"] = response.User;
                    Session.Timeout = 180;
                    return RedirectToAction("Index", "Home");
                }
                return Error(RedirectToAction("Index", "Home"), "Username of wachtwoord is incorrect");
            }
            return Error(RedirectToAction("Index", "Home"), "Username of wachtwoord is vereist");
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult RegisterUser(User user)
        {
            if(ModelState.IsValid) {
                user.Password = Encryption.Encrypt(user.Password);
                User addedUser = HttpManager.WebservicePostRequest<User>("User/Register", Request, user);
                return Message(RedirectToAction("Index", "Home"),"Gebruiker aangemaakt");
            }
            else {
                return View("Register", user);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return Error(RedirectToAction("Index", "Home"), "Succelvol uitgelogd");
        }
    }
}
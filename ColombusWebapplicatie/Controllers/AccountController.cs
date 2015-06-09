using ColombusWebapplicatie.Classes;
using ColombusWebapplicatie.Classes.Http;
using ColombusWebapplicatie.Models;
using System;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("EditUser", "Account");
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
                return MessageToIndex("Gebruiker aangemaakt");
            }
            else {
                return View("Register", user);
            }
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            if (Convert.ToBoolean(Session["LoggedIn"]))
            {
                if (ModelState.IsValid) {
                    user.Password = Encryption.Encrypt(user.Password);
                    User addedUser = HttpManager.WebservicePostRequest<User>("User", Request, user);
                    if (addedUser!=null) {
                        Session["User"] = addedUser;
                    }
                    return MessageToIndex("Gebruikersgegevens zijn succesvol aangepast.");
                }
                return View(user);
            }
            else
            {
                return Error(RedirectToAction("Index", "Home"), "U bent niet ingelogd.");
            }
        }

        [HttpGet]
        public ActionResult EditUser()
        {
            if(Convert.ToBoolean(Session["LoggedIn"]))
            {
                User user = (User) Session["User"];

                return View(user);
            }
            else
            {
                return Error(RedirectToAction("Index", "Home"), "U bent niet ingelogd.");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return Error(RedirectToAction("Index", "Home"), "Succelvol uitgelogd");
        }
    }
}
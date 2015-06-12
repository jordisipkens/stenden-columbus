using ColombusWebapplicatie.Classes;
using ColombusWebapplicatie.Classes.Http;
using ColombusWebapplicatie.Models;
using System;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class AccountController : BaseController
    {
        /// <summary>
        /// Show a detailed overview of the current User.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return RedirectToAction("EditUser", "Account");
        }

        /// <summary>
        /// Tries to Login and set the necessary Session Properties. Redirects to Home.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Shows a page for new users to register.
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Tries to save the new user. Returns to the Register View if there are errors.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult RegisterUser(User user)
        {
            if(ModelState.IsValid) {
                user.Password = Encryption.Encrypt(user.Password);
                User addedUser = HttpManager.WebservicePostRequest<User>("User/Register", Request, user);
                return Message(RedirectToAction("Index", "Home"), "Gebruiker aangemaakt");
            }
            else {
                return View("Register", user);
            }
        }

        /// <summary>
        /// Shows a View to edit the current user's details.
        /// </summary>
        /// <returns></returns>
        public ActionResult EditUser()
        {
            if(Convert.ToBoolean(Session["LoggedIn"])) {
                User user = (User)Session["User"];

                return View(user);
            }
            else {
                return Error(RedirectToAction("Index", "Home"), "U bent niet ingelogd.");
            }
        }

        /// <summary>
        /// Edit the current user's details.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUser(User user)
        {
            if(Convert.ToBoolean(Session["LoggedIn"])) {
                if(ModelState.IsValid) {
                    user.Password = Encryption.Encrypt(user.Password);
                    User addedUser = HttpManager.WebservicePostRequest<User>("User", Request, user);
                    if(addedUser != null) {
                        Session["User"] = addedUser;
                    }
                    return MessageToIndex("Gebruikersgegevens zijn succesvol aangepast.");
                }
                return View(user);
            }
            else {
                return Error(RedirectToAction("Index", "Home"), "U bent niet ingelogd.");
            }
        }

        /// <summary>
        /// Resets the Session and therefore loggin-out.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Abandon();
            return Error(RedirectToAction("Index", "Home"), "Succelvol uitgelogd");
        }
    }
}
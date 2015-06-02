using ColombusWebapplicatie.Classes;
using ColombusWebapplicatie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class AccountController : BaseController
    {
        //
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
                User addedUser = HTTPManager.GetRequest<User>("User", Request);
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
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

        [HttpPost]
        public string Login(User user)
        {
            WebRequest request = WebRequest.Create(apiUrl + "/api/User/Login");
            string userInfo = string.Format("{0}:{1}", user.Username, Encrypt(user.Password));
            string encodedUserInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(userInfo));
            string credentials = string.Format("{0} {1}", "Basic", encodedUserInfo);
            request.Headers["Authorization"] = credentials;
            WebResponse response = request.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            Token token = JsonConvert.DeserializeObject<Token>(streamReader.ReadToEnd());
            return token.TokenString;
        }

    }
}

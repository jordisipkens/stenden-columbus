using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebserviceColumbus.Authorization;

namespace WebserviceColumbus.Controllers
{
    public class UserController : ApiController
    {
        //GET: api/User/Register?username=..&password=..
        public HttpResponse Register(string username, string password)
        {
            return null;
        }

        //GET: api/User/Login
        [HttpGet]
        public HttpResponseMessage Login()
        {
            string result = TokenManager.CreateToken(HttpContext.Current);
            if (result != null) {
                return Request.CreateResponse(HttpStatusCode.OK,
                    result
                );
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [HttpGet, TokenRequired]
        //GET: api/User/Details?userID=..
        public HttpResponse Details(int userID)
        {
            return null;
        }

        //POST :api/User/Update
        [HttpPost, TokenRequired]
        public HttpResponse Update([FromBody]string value)
        {
            return null;
        }
    }
}

using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebserviceColumbus.Authorization;
using WebserviceColumbus.Database;
using WebserviceColumbus.Models.Other;

namespace WebserviceColumbus.Controllers
{
    public class UserController : ApiController
    {
        //POST: api/User/Register
        [HttpPost]
        public HttpResponseMessage Register([FromBody]User user)
        {
            if(user != null) {
                User foundUser = new UserDbManager().AddEntity(user);
                if(foundUser != null) {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        }

        //GET: api/User/Login
        [HttpGet]
        public HttpResponseMessage Login()
        {
            LoginResponse result = TokenManager.CreateToken();
            if(result != null) {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        //GET: api/User/Details/..
        [HttpGet, TokenRequired, Route("api/User/Details/{userID}")]
        public HttpResponseMessage Details(int userID)
        {
            UserDbManager dbManager = new UserDbManager();
            if(dbManager.ValidateUser(TokenManager.GetUsernameFromToken(), userID)) {
                return Request.CreateResponse(HttpStatusCode.OK, dbManager.GetEntity(userID));
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        //POST :api/User
        [HttpPost, TokenRequired, Route("api/User")]
        public HttpResponseMessage Update([FromBody]User user)
        {
            if(user != null) {
                return Request.CreateResponse(HttpStatusCode.Accepted, new UserDbManager().UpdateOrInsertEntity(user));
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        }
    }
}
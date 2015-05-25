using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebserviceColumbus.Authorization;
using WebserviceColumbus.Database;

namespace WebserviceColumbus.Controllers
{
    public class UserController : ApiController
    {
        //POST: api/User/Register
        [HttpPost]
        public HttpResponse Register([FromBody]string value)
        {
            return null;
        }

        //GET: api/User/Login
        [HttpGet]
        public HttpResponseMessage Login()
        {
            string result = TokenManager.CreateToken();
            if(result != null) {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [HttpGet, TokenRequired]
        //GET: api/User/Details?userID=..
        public HttpResponseMessage Details(int userID)
        {
            if(UserManager.ValidateUser(TokenManager.GetUsernameFromToken(), userID)) {
                return Request.CreateResponse(HttpStatusCode.OK, UserManager.GetEntity(userID));
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        //POST :api/User/Update
        [HttpPost, TokenRequired]
        public HttpResponse Update([FromBody]string value)
        {
            return null;
        }
    }
}
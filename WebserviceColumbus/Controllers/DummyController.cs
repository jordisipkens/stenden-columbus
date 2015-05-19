using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using WebserviceColumbus.Authorization;
using WebserviceColumbus.Classes.Encryption;
using WebserviceColumbus.Classes.IO;
using WebserviceColumbus.Models;
using WebserviceColumbus.Models.Travel;

namespace WebserviceColumbus.Controllers
{
    public class DummyController : ApiController
    {
        // GET: api/Dummy/Login?username=..&password=..
        [HttpGet]
        public HttpResponseMessage Login()
        {
            string test = "12-12-00:Roy";
            string test1 = Hash.Encrypt(test, AuthorizationDictionary.REALM);
            string test2 = Hash.Decrypt(test1, AuthorizationDictionary.REALM);


            string result = TokenManager.CreateToken(HttpContext.Current);
            if (result != null) {
                return Request.CreateResponse(HttpStatusCode.OK,
                    result
                );
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        //GET: api/Dummy/GetTravel?index=..
        public HttpResponseMessage GetTravel(int id)
        {
            if (TokenManager.IsAuthorized(HttpContext.Current)) {
                if (id > 1 || id < 0) {
                    id = 1;
                }
                Travel travel = JsonSerialization.Deserialize<List<Travel>>(IOManager.ReadFile(IOManager.GetProjectFilePath("Resources/DummyData/Travel.json")))[id];
                return Request.CreateResponse(HttpStatusCode.OK,
                    JsonSerialization.Serialize(travel)
                );
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        //GET: api/Dummy/GetAllTravels
        public HttpResponseMessage GetAllTravels()
        {
            if (TokenManager.IsAuthorized(HttpContext.Current)) {
                return Request.CreateResponse(HttpStatusCode.OK,
                    IOManager.ReadFile(IOManager.GetProjectFilePath("Resources/DummyData/Travel.json"))
                );
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        // GET: api/Dummy/GetTravelOgue
        public HttpResponseMessage GetTravelOgue()
        {
            if (TokenManager.IsAuthorized(HttpContext.Current)) {
                return Request.CreateResponse(HttpStatusCode.OK, 
                    IOManager.ReadFile(IOManager.GetProjectFilePath("Resources/DummyData/TravelOgue.json"))
                );
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        // GET: api/Dummy/GetUserInfo
        [HttpGet]
        public HttpResponseMessage GetUserInfo()
        {
            if (TokenManager.IsAuthorized(HttpContext.Current)) {
                return Request.CreateResponse(HttpStatusCode.OK, JsonSerialization.Serialize(new Models.User() {
                    ID = 0,
                    FirstName = "Jan",
                    LastName = "Lange",
                    Email = "lange.jan@email.com"
                }));
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}
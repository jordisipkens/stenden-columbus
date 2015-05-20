using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebserviceColumbus.Authorization;
using WebserviceColumbus.Classes.IO;
using WebserviceColumbus.Models.Travel;

namespace WebserviceColumbus.Controllers
{
    public class DummyController : ApiController
    {
        // GET: api/Dummy/Login?username=..&password=..
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

        //GET: api/Dummy/GetTravel?index=..
        [HttpGet, TokenRequired]
        public HttpResponseMessage GetTravel(int id)
        {
            if (id > 1 || id < 0) {
                id = 1;
            }
            Travel travel = JsonSerialization.Deserialize<List<Travel>>(IOManager.ReadFile(IOManager.GetProjectFilePath("Resources/DummyData/Travel.json")))[id];
            return Request.CreateResponse(HttpStatusCode.OK,
                JsonSerialization.Serialize(travel)
            );
        }

        //GET: api/Dummy/GetAllTravels
        [HttpGet, TokenRequired]
        public HttpResponseMessage GetAllTravels()
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                IOManager.ReadFile(IOManager.GetProjectFilePath("Resources/DummyData/Travel.json"))
            );
        }

        // GET: api/Dummy/GetTravelOgue
        [HttpGet, TokenRequired]
        public HttpResponseMessage GetTravelOgue()
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                IOManager.ReadFile(IOManager.GetProjectFilePath("Resources/DummyData/TravelOgue.json"))
            );
        }

        // GET: api/Dummy/GetUserInfo
        [HttpGet, TokenRequired]
        public HttpResponseMessage GetUserInfo()
        {
            return Request.CreateResponse(HttpStatusCode.OK, JsonSerialization.Serialize(new Models.User() {
                ID = 0,
                FirstName = "Jan",
                LastName = "Lange",
                Email = "lange.jan@email.com"
            }));
        }
    }
}
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebserviceColumbus.Authorization;
using WebserviceColumbus.IO;
using WebserviceColumbus.Models.Other;
using WebserviceColumbus.Models.Travel;

namespace WebserviceColumbus.Controllers
{
    public class DummyController : ApiController
    {
        // GET: api/Dummy/Login?username=..&password=..
        [HttpGet]
        public HttpResponseMessage Login()
        {
            string result = TokenManager.CreateToken();
            if(result != null) {
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
            List<Travel> travels = JsonSerialization.DeserializeFromFile<List<Travel>>("Resources/DummyData/Travel.json");
            if(id < 0 || id > travels.Count - 1) {
                return Request.CreateResponse(HttpStatusCode.OK, new Error() {
                    Message = "Index out of bounds"
                });
            }
            else {
                return Request.CreateResponse(HttpStatusCode.OK, travels[id]);
            }
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
            return Request.CreateResponse(HttpStatusCode.OK, new User() {
                ID = 0,
                FirstName = "Jan",
                LastName = "Lange",
                Email = "lange.jan@email.com"
            });
        }
    }
}
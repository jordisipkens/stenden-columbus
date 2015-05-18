using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Http;
using WebserviceColumbus.Classes.IO;
using WebserviceColumbus.Models;
using WebserviceColumbus.Models.Travel;

namespace WebserviceColumbus.Controllers
{
    public class DummyController : ApiController
    {
        //GET: api/Dummy/GetTravel?index=..
        [Authorize]
        public string GetTravel(int index)
        {
            if (index > 1 || index < 0) {
                index = 1;
            }
            Travel travel = JsonSerialization.Deserialize<List<Travel>>(IOManager.ReadFile(IOManager.GetProjectFilePath("Resources/DummyData/Travel.json")))[index];
            string result = JsonSerialization.Serialize(travel);
            if (result != null) {
                return result;
            }
            else {
                return JsonSerialization.Serialize(new Error() {
                    ErrorID = 204,
                    Message = "No travels found"
                });
            }
        }

        //GET: api/Dummy/GetAllTravels
        [Authorize]
        public string GetAllTravels()
        {
            string result = IOManager.ReadFile(IOManager.GetProjectFilePath("Resources/DummyData/Travel.json"));
            if (result != null) {
                return result;
            }
            else {
                return JsonSerialization.Serialize(new Error() {
                    ErrorID = 204,
                    Message = "No travels found"
                });
            }
        }

        // GET: api/Dummy/GetTravelOgue
        [Authorize]
        public string GetTravelOgue()
        {
            string result = IOManager.ReadFile(IOManager.GetProjectFilePath("Resources/DummyData/TravelOgue.json"));
            if (result != null) {
                return result;
            }
            else {
                return JsonSerialization.Serialize(new Error() {
                    ErrorID = 204,
                    Message = "No travelogue's found"
                });
            }
        }

        // GET: api/Dummy/Login?username=..&password=..
        public void Login(string username, string password)
        {
            string uri = "http://localhost:2758/api/Travel";
            WebRequest req = WebRequest.Create(uri);
            string userInfo = string.Format("{0}:{1}", username, password);
            string encodedUserInfo = Convert.ToBase64String(Encoding.GetEncoding("iso-8859-1").GetBytes(userInfo));
            string credentials = string.Format("{0} {1}", "Basic", encodedUserInfo);
            req.Headers["Authorization"] = credentials;
            System.Net.WebResponse response = req.GetResponse();
        }

        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Dummy
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Dummy/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}

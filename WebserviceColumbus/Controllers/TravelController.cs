using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebserviceColumbus.Authorization;
using WebserviceColumbus.Database;
using WebserviceColumbus.Models.Travel;

namespace WebserviceColumbus.Controllers
{
    public class TravelController : ApiController
    {
        // GET: api/Travel/Get?userID=..&travelID=..
        [HttpGet, TokenRequired]
        public HttpResponseMessage Get(int travelID)
        {
            Travel travel = DbManager<Travel>.GetEntity(travelID);
            if(travel != null) {
                if(travel.User.Username.Equals(TokenManager.GetUsernameFromToken())) {
                    return Request.CreateResponse(HttpStatusCode.OK, travel);
                }
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        }

        // GET: api/Travel?userID=..&offset=..&limit=..
        [HttpGet, TokenRequired]
        public HttpResponseMessage GetAll(int userID, int offset = 0, int limit = 20)
        {
            if(UserDbManager.ValidateUser(TokenManager.GetUsernameFromToken(), userID)) {
                return Request.CreateResponse(HttpStatusCode.OK, TravelDbManager.GetAllTravels(userID, offset, limit));
            }
            return Request.CreateResponse(HttpStatusCode.Forbidden);
        }

        // POST: api/Travel/Update
        [HttpPost, TokenRequired]
        public HttpResponseMessage Update([FromBody]Travel travel)
        {
            if(travel != null) {
                if(DbManager<Travel>.UpdateEntity(travel)) {
                    return Request.CreateResponse(HttpStatusCode.Accepted);
                }
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        }

        // GET: api/Travel/Create
        [HttpGet, TokenRequired]
        public HttpResponseMessage Create([FromBody]Travel travel, int userID)
        {
            if(UserDbManager.ValidateUser(TokenManager.GetUsernameFromToken(), userID)) {
                if(TravelDbManager.AddEntity(travel)) {
                    return Request.CreateResponse(HttpStatusCode.OK, travel);
                }
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
            }
            return Request.CreateResponse(HttpStatusCode.Forbidden);
        }

        // GET: api/Travel/Delete
        [HttpGet, TokenRequired]
        public HttpResponseMessage Delete(int travelID)
        {
            Travel travel = TravelDbManager.GetEntity(travelID);
            if(travel != null) {
                if(travel.User.Username.Equals(TokenManager.GetUsernameFromToken())) {
                    if(TravelDbManager.DeleteEntity(travelID)) {
                        return Request.CreateResponse(HttpStatusCode.Accepted);
                    }
                    return Request.CreateResponse(HttpStatusCode.Conflict);
                }
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        }
    }
}
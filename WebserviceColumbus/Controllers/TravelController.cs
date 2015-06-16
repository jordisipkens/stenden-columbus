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
        // GET: api/Travel/..
        [HttpGet, TokenRequired, Route("api/Travel/{travelID}")]
        public HttpResponseMessage Get(int travelID)
        {
            Travel travel = new TravelDbManager().GetEntity(travelID);
            if(travel != null && !travel.User.Username.Equals(TokenManager.GetUsernameFromToken())) {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            return Request.CreateResponse(HttpStatusCode.OK, travel);
        }

        // GET: api/Travel/GetAll?userID=..&offset=..&limit=..
        [HttpGet, TokenRequired, Route("api/Travel/GetAll")]
        public HttpResponseMessage GetAll(int userID, int offset = 0, int limit = 20)
        {
            if(new UserDbManager().ValidateUser(TokenManager.GetUsernameFromToken(), userID)) {
                return Request.CreateResponse(HttpStatusCode.OK, new TravelDbManager().GetEntities(userID, offset, limit));
            }
            return Request.CreateResponse(HttpStatusCode.Forbidden);
        }

        // POST: api/Travel
        [HttpPost, TokenRequired, Route("api/Travel")]
        public HttpResponseMessage Update([FromBody]Travel travel)
        {
            if(travel != null) {
                return Request.CreateResponse(HttpStatusCode.OK, new TravelDbManager().TempUpdateOrInsertEntity(travel));
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        }

        // GET: api/Travel/Delete/..
        [HttpGet, TokenRequired, Route("api/Travel/Delete/{travelID}")]
        public HttpResponseMessage Delete(int travelID)
        {
            TravelDbManager dbManager = new TravelDbManager();
            Travel travel = dbManager.GetEntity(travelID);
            if(travel != null) {
                if(travel.User.Username.Equals(TokenManager.GetUsernameFromToken())) {
                    if(dbManager.DeleteEntity(travel)) {
                        return Request.CreateResponse(HttpStatusCode.Accepted);
                    }
                    return Request.CreateResponse(HttpStatusCode.Conflict);
                }
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        }

        // GET: api/Travel/DeleteLocation?travelID=..&locationID=..
        [HttpGet, TokenRequired, Route("api/Travel/DeleteLocation")]
        public HttpResponseMessage DeleteLocation(int travelID, int locationID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new TravelDbManager().DeleteLocation(travelID, locationID));
        }
    }
}

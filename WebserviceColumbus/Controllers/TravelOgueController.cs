using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebserviceColumbus.Authorization;
using WebserviceColumbus.Database;
using WebserviceColumbus.Models.Travel;
using WebserviceColumbus.Models.Travel.Travelogue;

namespace WebserviceColumbus.Controllers
{
    public class TravelOgueController : ApiController
    {
        //GET: api/Travelogue/..
        [HttpGet, TokenRequired, Route("api/Travelogue/{travelogueID}")]
        public HttpResponseMessage Get(int travelogueID)
        {
            Travelogue travelogue = new TravelogueDbManager().GetEntity(travelogueID);
            if(travelogue != null) {
                if(new UserDbManager().ValidateUser(TokenManager.GetUsernameFromToken(), new TravelDbManager().GetEntity(travelogue.TravelID).UserID)) {
                    return Request.CreateResponse(HttpStatusCode.OK, travelogue);
                }
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        }

        //GET: api/Travelogue/GetAll?userID=..
        [HttpGet, TokenRequired, Route("api/Travelogue/GetAll")]
        public HttpResponseMessage GetAll(int userID)
        {
            if(new UserDbManager().ValidateUser(TokenManager.GetUsernameFromToken(), userID)) {
                List<Travelogue> travelogues = new TravelogueDbManager().GetEntities(userID);
                if(travelogues != null) {
                    return Request.CreateResponse(HttpStatusCode.OK, travelogues);
                }
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
            }
            return Request.CreateResponse(HttpStatusCode.Forbidden);
        }

        //POST: api/Travelogue
        [HttpPost, TokenRequired, Route("api/Travelogue")]
        public HttpResponseMessage Update([FromBody]Travelogue travelogue)
        {
            if(travelogue != null) {
                return Request.CreateResponse(HttpStatusCode.Accepted, new TravelogueDbManager().UpdateOrInsertEntity(travelogue));
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        }

        //GET: api/Travelogue/Delete?travelogueID=..
        [HttpGet, TokenRequired, Route("api/Travelogue/Delete/{travelogueID}")]
        public HttpResponseMessage Delete(int travelogueID)
        {
            TravelogueDbManager dbManager = new TravelogueDbManager();
            Travelogue travelogue = dbManager.GetEntity(travelogueID);
            if(travelogue != null) {
                Travel travel = new TravelDbManager().GetEntity(travelogue.TravelID);
                if(travel != null) {
                    if(travel.User.Username.Equals(TokenManager.GetUsernameFromToken())) {
                        if(dbManager.DeleteEntity(travelogue)) {
                            return Request.CreateResponse(HttpStatusCode.OK);
                        }
                        return Request.CreateResponse(HttpStatusCode.Conflict);
                    }
                    return Request.CreateResponse(HttpStatusCode.Forbidden);
                }
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        }

        //GET: api/Travelogue/Display?filter=..&offset=..&limit=..
        [HttpGet, Route("api/Travelogue/Display")]
        public HttpResponseMessage Display(SearchType filter = SearchType.Latest, int offset = 0, int limit = 20)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new TravelogueDbManager().Display(filter, offset, limit));
        }

        //GET: api/Travelogue/Search?value=..&limit=..
        [HttpGet, Route("api/Travelogue/Search")]
        public HttpResponseMessage Search(string value, int limit = 20)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new TravelogueDbManager().Search(value, limit));
        }

        //GET: api/Travelogue?travelogueID=..&isPublic=..
        [HttpGet, TokenRequired, Route("api/Travelogue/Publish")]
        public HttpResponseMessage Publish(int travelogueID, bool isPublic = true)
        {
            if(new TravelogueDbManager().Publish(travelogueID, isPublic)) {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.Conflict);
        }

        //POST: api/Travelogue/Rate?travelogueID=..&rating=..
        [HttpGet, Route("api/Travelogue/Rate")]
        public HttpResponseMessage Rate(int travelogueID, double rating)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new TravelogueDbManager().Rate(travelogueID, rating));
        }
    }
}

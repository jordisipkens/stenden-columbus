using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebserviceColumbus.Authorization;
using WebserviceColumbus.Database;
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
            if(new UserDbManager().ValidateUser(TokenManager.GetUsernameFromToken(), new TravelDbManager().GetEntity(travelogue.TravelID).UserID)) {
                return Request.CreateResponse(HttpStatusCode.OK, travelogue);
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

        //GET: api/Travelogue/Display?filter=..&offset=..&limit=..
        [HttpGet]
        public HttpResponseMessage Display(SearchType filter = SearchType.Latest, int offset = 0, int limit = 20)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new TravelogueDbManager().Display(filter, offset, limit));
        }

        //GET: api/Travelogue/Search?value=..&limit=..
        [HttpGet]
        public HttpResponseMessage Search(string value, int limit = 20)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new TravelogueDbManager().Search(value, limit));
        }

        //GET: api/Travelogue?travelogueID=..&isPublic=..
        [HttpGet, TokenRequired]
        public HttpResponseMessage Publish(int travelogueID, bool isPublic = true)
        {
            if(new TravelogueDbManager().Publish(travelogueID, isPublic)) {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.Conflict);
        }

        //POST: api/Travelogue/Rate?travelogueID=..&rating=..
        [HttpGet]
        public HttpResponseMessage Rate(int travelogueID, double rating)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new TravelogueDbManager().Rate(travelogueID, rating));
        }
    }
}
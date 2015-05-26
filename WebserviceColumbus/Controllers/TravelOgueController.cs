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
            return Request.CreateResponse(HttpStatusCode.OK, TravelogueDbManager.GetTravelogue(travelogueID));
        }

        //POST: api/Travelogue
        [HttpPost, TokenRequired]
        public HttpResponseMessage Update([FromBody]Travelogue travelogue)
        {
            if(travelogue != null) {
                return Request.CreateResponse(HttpStatusCode.Accepted, TravelogueDbManager.UpdateOrAdd(travelogue));
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        }

        //GET: api/Travelogue/Display?filter=..&offset=..&limit=..
        [HttpGet]
        public HttpResponseMessage Display(SearchType filter = SearchType.Latest, int offset = 0, int limit = 20)
        {
            return Request.CreateResponse(HttpStatusCode.OK, TravelogueDbManager.Display(filter, offset, limit));
        }

        //GET: api/Travelogue/Search?value=..&limit=..
        [HttpGet]
        public HttpResponseMessage Search(string value, int limit = 20)
        {
            return Request.CreateResponse(HttpStatusCode.OK, TravelogueDbManager.Search(value, limit));
        }

        //GET: api/Travelogue?travelogueID=..&isPublic=..
        [HttpGet, TokenRequired]
        public HttpResponseMessage Publish(int travelogueID, bool isPublic = true)
        {
            return null;
        }
    }
}
﻿using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebserviceColumbus.Authorization;
using WebserviceColumbus.Database;
using WebserviceColumbus.Models.Other;
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
            //TODO Add security check if the travelogue has been published. If not check user
        }

        //POST: api/Travelogue
        [HttpPost, TokenRequired, Route("api/Travelogue")]
        public HttpResponseMessage Update([FromBody]Travelogue travelogue)
        {
            if(travelogue != null) {
                return Request.CreateResponse(HttpStatusCode.Accepted, TravelogueDbManager.UpdateOrInsert(travelogue));
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
            if(TravelogueDbManager.Publish(travelogueID, isPublic)) {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.Conflict);
        }

        //POST: api/Travelogue/Rate?travelogueID=..&rating=..
        [HttpGet]
        public HttpResponseMessage Rate(int travelogueID, double rating)
        {
            return Request.CreateResponse(HttpStatusCode.NotImplemented);
        }
    }
}
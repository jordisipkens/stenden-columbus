using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebserviceColumbus.Authorization;

namespace WebserviceColumbus.Controllers
{
    public class TravelController : ApiController
    {
        // GET: api/Travel/Get?userID=..&travelID=..
        [HttpGet, TokenRequired]
        public HttpResponseMessage Get(int travelID)
        {
            return null;
        }

        // GET: api/Travel?userID=..&offset=..&limit=..
        [HttpGet, TokenRequired]
        public HttpResponseMessage GetAll(int userID, int offset = 0, int limit = 20)
        {
            if (offset > 100) {
                offset = 100;
            }
            return null;
        }

        // POST: api/Travel/Update
        [HttpPost, TokenRequired]
        public HttpResponseMessage Update([FromBody]string value)
        {
            return null;
        }

        // GET: api/Travel/Delete
        [HttpGet, TokenRequired]
        public HttpResponseMessage Delete(int travelID)
        {
            return null;
        }

        [HttpGet, TokenRequired]
        public HttpResponseMessage Create(int userID)
        {
            return null;
        }
    }
}

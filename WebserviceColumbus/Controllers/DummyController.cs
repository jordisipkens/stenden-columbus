using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebserviceColumbus.Controllers
{
    public class DummyController : ApiController
    {
        // GET: api/Dummy
        public string Get()
        {
            return WebserviceColumbus.Classes.IO.IOManager.ReadFile(@"D:\Code Projects\WebserviceColumbus\WebserviceColumbus\DummyData\Travel.json");
        }

        // GET: api/Dummy/5
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

        // DELETE: api/Dummy/5
        public void Delete(int id)
        {
        }
    }
}

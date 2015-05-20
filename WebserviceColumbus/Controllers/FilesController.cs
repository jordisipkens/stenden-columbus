using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebserviceColumbus.Authorization;

namespace WebserviceColumbus.Controllers
{
    public class FilesController : ApiController
    {
        // GET: api/Files
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Files/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Files
        [Route("api/Files/Images"), TokenRequired]
        public HttpResponseMessage Post([FromBody]string value)
        {
            HttpFileCollection httpRequestFiles = HttpContext.Current.Request.Files;
            if (httpRequestFiles.Count > 0) {
                foreach (string file in httpRequestFiles) {
                    var postedFile = httpRequestFiles[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    Console.WriteLine(string.Format("SIMULATED UPLOAD FILE: {0}", filePath));    //Real code: postedFile.SaveAs(filePath);  
                    //TODO
                }
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // DELETE: api/Files/5
        public void Delete(int id)
        {
        }
    }
}

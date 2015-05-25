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
        // POST: api/Files/Images
        [HttpPost, TokenRequired, Route("api/Files/Images")]
        public HttpResponseMessage Post([FromBody]string value)
        {
            HttpFileCollection httpRequestFiles = HttpContext.Current.Request.Files;
            if(httpRequestFiles.Count > 0) {
                foreach(string file in httpRequestFiles) {
                    var postedFile = httpRequestFiles[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    Console.WriteLine(string.Format("SIMULATED UPLOAD FILE: {0}", filePath));
                    //TODO postedFile.SaveAs(filePath);
                }
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
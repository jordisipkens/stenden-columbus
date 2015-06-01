using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebserviceColumbus.Authorization;
using WebserviceColumbus.Database;
using WebserviceColumbus.Models.Other;

namespace WebserviceColumbus.Controllers
{
    public class FilesController : ApiController
    {
        // POST: api/Files/Images
        [HttpPost, TokenRequired, Route("api/Files/Images")]
        public HttpResponseMessage SaveImage([FromBody]string value)
        {
            HttpFileCollection httpRequestFiles = HttpContext.Current.Request.Files;
            if(httpRequestFiles.Count > 0) {
                foreach(string file in httpRequestFiles) {
                    var postedFile = httpRequestFiles[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    Console.WriteLine(string.Format("SIMULATED UPLOAD FILE: {0}", filePath));       //TODO Change: postedFile.SaveAs(filePath);
                    return Request.CreateResponse(HttpStatusCode.OK, new Photo() {
                        URL = filePath
                    });
                }
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        }

        [HttpGet, Route("api/File/Images/{photoID}")]
        public HttpResponseMessage GetPhoto(int photoID)
        {
            Photo photo = new DbManager<Photo>().GetEntity(photoID);
            if(photo != null) {
                return Request.CreateResponse(HttpStatusCode.OK, photo);
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        }
    }
}
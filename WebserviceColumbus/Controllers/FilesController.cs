using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebserviceColumbus.Authorization;
using WebserviceColumbus.Database;
using WebserviceColumbus.Models.Other;
using WebserviceColumbus.Other;

namespace WebserviceColumbus.Controllers
{
    public class FilesController : ApiController
    {
        // POST: api/Files/Images
        [HttpPost, TokenRequired, Route("api/Files/Images/{userID}")]
        public HttpResponseMessage SaveImage(int userID)
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            if(files.Count > 0) {
                User user = new DbManager<User>().GetEntity(userID);
                if(user != null) {
                    List<Photo> photos = new List<Photo>();
                    foreach(string file in files) {
                        photos.Add(FileManager.UploadFile(user, files[file]));
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, photos);
                }
            }
            return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        }

        [HttpGet, Route("api/Files/Images/{photoID}")]
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
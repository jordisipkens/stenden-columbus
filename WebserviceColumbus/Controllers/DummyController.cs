using System.Web.Http;
using WebserviceColumbus.Classes.Encryption;
using WebserviceColumbus.Classes.IO;
using WebserviceColumbus.Models;

namespace WebserviceColumbus.Controllers
{
    public class DummyController : ApiController
    {
        [Authorize]
        // GET: api/Dummy
        public string Get()
        {
            string result = IOManager.ReadFile(IOManager.GetProjectFilePath("DummyData/Travel.json"));
            if(result != null) {
                return result;
            }
            else {
                return JsonSerialization.Serialization(new Error() {
                    ErrorID = 204,
                    Message = "No travels found"
                });
            }
        }

        [HttpGet]
        public string Login(string username, string password)
        {
            if (username.Equals("columbus") && Hash.HashText(password, username).Equals("ahE0/lFr7o3HH6zTJsw7QniGfgo=")) {
                return string.Empty;
            }
            else {
                return JsonSerialization.Serialization(new Error() {
                    ErrorID = 401,
                    Message = "Invalid password"
                });
            }
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

using ColombusWebapplicatie.Classes.Http;
using ColombusWebapplicatie.Models.Google;
using ColombusWebapplicatie.Models.Travel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class TravelController : BaseController
    {
        // GET: /Travel/

        // View all travels.
        public ActionResult Index()
        {
            // Load Json file.
            StreamReader streamReader = new StreamReader(Server.MapPath("~/Content/json/Travel.json"));
            // Deserialize Json to list of Travel objects.
            List<Travel> travels = JsonConvert.DeserializeObject<List<Travel>>(streamReader.ReadToEnd());
            // Return the regarding view.
            return View(travels);
        }

        public ActionResult View(int id = 0)
        {
            if(id != 0) {
                List<Travel> travels = JsonSerialization.DeserializeFromFile<List<Travel>>(Server.MapPath("~/Content/json/Travel.json"));
                /*Travel travel = HTTPManager.WebserviceGetRequest<Travel>("travel/" + id, Request);
                if(travel != null) {*/
                //}
                Travel travel = travels.Find(i => i.ID == id);
                if (travel !=null) {
                    return View(travel);
                }
            }
            return ErrorToIndex("Deze reis bestaat niet (meer)");
        }

        private GoogleResponse RequestGooglePlaces(string query)
        {
            string googleApiKey = "AIzaSyDpXa5VtOKNRA8obETZwkV7vbHzjio-17k";
            string googleUrl = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=" + query + "&key=" + googleApiKey;
            WebRequest request = WebRequest.Create(googleUrl);
            WebResponse response = request.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            string responseString = streamReader.ReadToEnd();
            GoogleResponse googlePlacesResponse = (GoogleResponse)JsonConvert.DeserializeObject<GoogleResponse>(responseString);
            return googlePlacesResponse;
        }

        [HttpGet] // Show add travel page
        public ActionResult AddTravel()
        {
            Travel travel = new Travel();
            return View(travel);
        }

        // Maar als je dan locaties hebt hoe zet je dat in een form? Als je 1 aan wilt klikken, dat
        // zijn dan links geen formulierelementen dus die worden niet meegenomen. Ja
        [HttpPost] // Add travel to webservice
        public ActionResult AddTravel(Travel travel)
        {
            if(ModelState.IsValid) {
                JsonSerialization.SerializeToFile(travel, (Server.MapPath("~/Content/json/AddTravel.json")));
                Travel savedTravel = JsonSerialization.DeserializeFromFile<Travel>(Server.MapPath("~/Content/json/AddTravel.json"));
                return RedirectToAction("AddLocations", "Travel", new { travelId = savedTravel.ID });
            }
            else {
                return View(travel);
            }
        }

        [HttpGet] // Show add locations page.
        public ActionResult AddLocations(int travelId)
        {
            // Supposed to try and get the travel by the travelId.
            Travel travel = JsonSerialization.DeserializeFromFile<Travel>(Server.MapPath("~/Content/json/AddTravel.json"));
            // Supposed to check if travel exists.
            if(travelId == travel.ID) {
                return View(travel);
            }
            else {
                return ErrorToIndex("Deze reis bestaat niet (meer).");
            }
        }
        [HttpPost] // Search for a location via Google Places API
        public ActionResult SearchLocation(string query)
        {
            GoogleResponse response = this.RequestGooglePlaces(query);
            return View(response);
        }

        
    }
}
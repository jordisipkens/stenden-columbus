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

        [HttpPost] // Search for a location via Google Places API
        public ActionResult SearchLocation(string query)
        {
            GoogleResponse response = this.RequestGooglePlaces(query);
            return View(response);
        }

        public ActionResult ViewTravel(int? id)
        {
            // !!! Important: Not finished, just for testing !!! Supposed to check if the travel
            // with the regarding id exists.
            if(id == null) {
                // Return to the index of the controller with an error message.
                return ErrorToIndex("Deze reis bestaat niet (meer).");
            }
            else {
                // Load Json file.
                StreamReader streamReader = new StreamReader(Server.MapPath("~/Content/json/Travel.json"));
                // Deserialize Json to list of Travel objects.
                List<Travel> travels = JsonConvert.DeserializeObject<List<Travel>>(streamReader.ReadToEnd());
                // !!! Important: Not finished, just for testing !!! Supposed to get the travel with
                // the regarding id.
                Travel travel = travels.First();
                // Return the regarding view.
                return View(travel);
            }
        }
    }
}
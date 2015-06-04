using ColombusWebapplicatie.Classes.Http;
using ColombusWebapplicatie.Models.Google;
using ColombusWebapplicatie.Models.Travel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class TravelController : BaseController
    {
        public ActionResult Index()
        {
            // Load Json file.
            StreamReader streamReader = new StreamReader(Server.MapPath("~/Content/json/Travel.json"));
            // Deserialize Json to list of Travel objects.
            List<Travel> travels = JsonConvert.DeserializeObject<List<Travel>>(streamReader.ReadToEnd());
            // Return the regarding view.
            return View(travels);
        }

        public ActionResult ViewTravel(int id)
        {
            if(id != 0) {
                List<Travel> travels = JsonSerialization.DeserializeFromFile<List<Travel>>(Server.MapPath("~/Content/json/Travel.json"));
                /*Travel travel = HTTPManager.WebserviceGetRequest<Travel>("travel/" + id, Request);
                if(travel != null) {*/
                return View(travels[id]);
                //}
            }
            return ErrorToIndex("Deze reis bestaat niet (meer)");
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateTravel(Travel travel)
        {
            if(ModelState.IsValid) {
                if(IsLoggedIn()) {
                    //travel.User = Session["User"] as User;
                    //Travel addedTravel = HTTPManager.WebservicePostRequest<Travel>("Travel", Request, travel);
                    return RedirectToAction("Index", "Travel");
                }
                return ErrorToIndex("Je bent niet ingelogd");
            }
            else {
                return View("Create", travel);
            }
        }

        public ActionResult AddLocation(int travelID)
        {
            if(travelID != 0) {
                ViewBag.TravelID = travelID;
                return View();
            }
            return ErrorToIndex("Deze reis bestaat niet (meer)");
        }

        [HttpPost]
        public ActionResult SearchLocation(int travelID, string query)
        {

            List<LocationDetails> locations = RequestGooglePlaces("textsearch", new Dictionary<string, string>() { { "query", query } });
            if(locations != null) {
                ViewBag.TravelID = travelID;
                return View("AddLocation", locations);
            }
            return Error(RedirectToAction("ViewTravel", travelID), "Er is een fout opgetreden tijdens het zoeken naar locaties");
        }

        private List<LocationDetails> RequestGooglePlaces(string url, Dictionary<string, string> parameters)
        {
            GoogleResponse response = HTTPManager.GoogleGetRequest<GoogleResponse>(url, parameters);
            if(response != null) {
                List<LocationDetails> locations = new List<LocationDetails>();
                foreach(GoogleResult result in response.Results) {
                    locations.Add(new LocationDetails(result));
                }
                return locations;
            }
            return null;
        }
    }
}
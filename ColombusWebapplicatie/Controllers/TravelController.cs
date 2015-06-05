using ColombusWebapplicatie.Classes.Http;
using ColombusWebapplicatie.Models;
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
            int userID = (Session["User"] as User).ID;
            List<Travel> travels = HTTPManager.WebserviceGetRequest<List<Travel>>("Travel/GetAll", Request, null, new Dictionary<string, string>() { { "userID", userID.ToString() } });
            return View(travels);
        }

        public ActionResult ViewTravel(int id = 0)
        {
            if(id != 0) {
                Travel travel = HTTPManager.WebserviceGetRequest<Travel>("travel/" + id, Request);
                if(travel != null) {
                    return View(travel);
                }
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
                travel.UserID = (Session["User"] as User).ID;
                Travel addedTravel = HTTPManager.WebservicePostRequest<Travel>("Travel", Request, travel);
                return RedirectToAction("Index", "Travel");
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
using ColombusWebapplicatie.Classes.Http;
using ColombusWebapplicatie.Models;
using ColombusWebapplicatie.Models.Google.Details;
using ColombusWebapplicatie.Models.Google.Search;
using ColombusWebapplicatie.Models.Travel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class TravelController : BaseController
    {
        public ActionResult Index()
        {
            if (Session["User"] != null && (Session["User"]).GetType() == typeof(User))
            {
                int userID = (Session["User"] as User).ID;
                List<Travel> travels = HTTPManager.WebserviceGetRequest<List<Travel>>("Travel/GetAll", Request, null, new Dictionary<string, string>() { { "userID", userID.ToString() } });
                return View(travels);
            }
            return View();
        }

        public ActionResult ViewTravel(int id = 0)
        {
            if (id != 0)
            {
                Travel travel = HTTPManager.WebserviceGetRequest<Travel>("travel/" + id, Request);
                if (travel != null)
                {
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
            if (ModelState.IsValid)
            {
                travel.UserID = (Session["User"] as User).ID;
                Travel addedTravel = HTTPManager.WebservicePostRequest<Travel>("Travel", Request, travel);
                return RedirectToAction("Index", "Travel");
            }
            else
            {
                return View("Create", travel);
            }
        }

        public ActionResult SearchLocation(int travelID)
        {
            if (travelID != 0)
            {
                ViewBag.TravelID = travelID;
                return View();
            }
            return ErrorToIndex("Deze reis bestaat niet (meer)");
        }

        public ActionResult ShowFoundLocation(int travelID, string query)
        {
            List<LocationDetails> locations = RequestGooglePlaces("textsearch", new Dictionary<string, string>() { { "query", query } });
            if (locations != null)
            {
                ViewBag.TravelID = travelID;
                return View("SearchLocation", locations);
            }
            return Error(RedirectToAction("ViewTravel", travelID), "Er is een fout opgetreden tijdens het zoeken naar locaties");
        }

        public ActionResult ViewLocation(int travelID, string placeID)
        {
            ViewBag.TravelID = travelID;
            GoogleDetailResponse response = HTTPManager.GoogleGetRequest<GoogleDetailResponse>("details", new Dictionary<string, string>() { { "placeid", placeID } });
            return View(response.Result);
        }

        public ActionResult AddLocation(int travelID, string date)
        {
            GooglePlace place = TempData["Place"] as GooglePlace;
            Travel travel = HTTPManager.WebserviceGetRequest<Travel>("travel/" + travelID, Request);
            travel.Locations.Add(new Location(place, DateTime.Parse(date)));
            Travel postedTravel = HTTPManager.WebservicePostRequest<Travel>("travel", Request, travel);
            return RedirectToAction("Index", "Travel"); //TODO Go to travel with corresponding TravelID
        }

        public ActionResult CreateNote(int travelID, int locationID, string note)
        {
            Travel travel = HTTPManager.WebserviceGetRequest<Travel>("travel/" + travelID, Request);
            foreach (Location location in travel.Locations)
            {
                if(location.ID == locationID)
                {
                    location.Note = note;
                    Travel postedTravel = HTTPManager.WebservicePostRequest<Travel>("travel", Request, travel);
                    return Message(RedirectToAction("ViewTravel", "Travel", new { id = travelID}), "Notitie toegevoegd");
                }
            }
            return Error(RedirectToAction("ViewTravel", "Travel", new { id = travelID }), "Er is iets fout gegaan.");
        }

        #region Helpers
        private List<LocationDetails> RequestGooglePlaces(string url, Dictionary<string, string> parameters)
        {
            GoogleSearchResponse response = HTTPManager.GoogleGetRequest<GoogleSearchResponse>(url, parameters);
            if (response != null)
            {
                List<LocationDetails> locations = new List<LocationDetails>();
                foreach (GoogleResult result in response.Results)
                {
                    locations.Add(new LocationDetails(result));
                }
                return locations;
            }
            return null;
        }
        #endregion
    }
}
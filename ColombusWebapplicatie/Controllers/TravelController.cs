using ColombusWebapplicatie.Classes.Http;
using ColombusWebapplicatie.Models.Google.Details;
using ColombusWebapplicatie.Models.Google.Search;
using ColombusWebapplicatie.Models.Travel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class TravelController : BaseController
    {
        /// <summary>
        /// Overview of all Travels.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if(GetCurrentUser() != null) {
                List<Travel> travels = HttpManager.WebserviceGetRequest<List<Travel>>("Travel/GetAll", Request, null, new Dictionary<string, string>() { { "userID", GetCurrentUser().ID.ToString() } });
                return View(travels);
            }
            return View();
        }

        /// <summary>
        /// Display a single detailed Travel.
        /// </summary>
        /// <param name="travelID"></param>
        /// <returns></returns>
        public ActionResult ViewTravel(int travelID = 0)
        {
            if(travelID != 0) {
                Travel travel = HttpManager.WebserviceGetRequest<Travel>("travel/" + travelID, Request);
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

        /// <summary>
        /// Creates a new Travel and returns to the Index.
        /// </summary>
        /// <param name="travel"></param>
        /// <returns></returns>
        public ActionResult CreateTravel(Travel travel)
        {
            if(ModelState.IsValid) {
                if(GetCurrentUser() != null) {
                    travel.UserID = GetCurrentUser().ID;
                    Travel addedTravel = HttpManager.WebservicePostRequest<Travel>("Travel", Request, travel);
                    return MessageToIndex("Reis is aangemaakt");
                }
            }
            return View("Create", travel);
        }

        /// <summary>
        /// Deletes the travel with the given corresponding travelID and returns to the overview.
        /// </summary>
        /// <param name="travelID"></param>
        /// <returns></returns>
        public ActionResult DeleteTravel(int travelID)
        {
            HttpManager.WebserviceGetRequest<Travel>("Travel/Delete/" + travelID, Request);
            return MessageToIndex("De reis is verwijderd");
        }

        /// <summary>
        /// Search locations to add to the travel.
        /// </summary>
        /// <param name="travelID"></param>
        /// <returns></returns>
        public ActionResult SearchLocation(int travelID)
        {
            if(travelID != 0) {
                ViewBag.TravelID = travelID;
                return View();
            }
            return ErrorToIndex("Deze reis bestaat niet (meer)");
        }

        /// <summary>
        /// Shows the found location by the query.
        /// </summary>
        /// <param name="travelID"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public ActionResult ShowFoundLocation(int travelID, string query, float? lat, float? lng, int? radius)
        {
            string searchType;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            // If a marker is placed and query is empty
            if (query.Length==0&&lat!=null&&lng!=null)
            {
                // Add parameters for nearby search
                searchType = "nearbysearch";
                parameters.Add("location", lat.ToString() + "," + lng.ToString());
                parameters.Add("radius", radius.ToString());
                ModelState.Remove("lat");   // Reset hidden field lat
                ModelState.Remove("lng");   // Reset hidden field lng
            }
            else
            {
                // Add parameters for text search
                searchType = "textsearch";
                parameters.Add("query", query);
            }
            List<LocationDetails> locations = RequestGooglePlaces(searchType, parameters);
            if(locations != null) {
                ViewBag.TravelID = travelID;
                return View("SearchLocation", locations);
            }
            return Error(RedirectToAction("ViewTravel", travelID), "Er is een fout opgetreden tijdens het zoeken naar locaties");
        }

        /// <summary>
        /// Shows a detailed overview of a single Location.
        /// </summary>
        /// <param name="travelID"></param>
        /// <param name="placeID"></param>
        /// <returns></returns>
        public ActionResult ViewLocation(int travelID, string placeID, bool newLocation = false)
        {
            ViewBag.TravelID = travelID;
            GoogleDetailResponse response = HttpManager.GoogleGetRequest<GoogleDetailResponse>("details", new Dictionary<string, string>() { { "placeid", placeID } });
            ViewBag.newLocation = newLocation;
            return View(response.Result);
        }

        /// <summary>
        /// Adds a Location to the Travel. Returns to the Travel detailed overview.
        /// </summary>
        /// <param name="travelID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public ActionResult AddLocation(int travelID, string date)
        {
            Travel travel = HttpManager.WebserviceGetRequest<Travel>("travel/" + travelID, Request);
            travel.Locations.Add(new Location(TempData["Place"] as GooglePlace, DateTime.Parse(date)));

            Travel postedTravel = HttpManager.WebservicePostRequest<Travel>("travel", Request, travel);
            return RedirectToAction("ViewTravel", "Travel", new { travelID = travelID });
        }

        /// <summary>
        /// Deletes the given Location from the Travel.
        /// </summary>
        /// <param name="travel"></param>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public ActionResult DeleteLocation(Travel travel, int locationID)
        {
            foreach(Location location in travel.Locations) {
                if(location.ID == locationID) {
                    locationID = -1;
                    break;
                }
            }
            HttpManager.WebservicePostRequest<Travel>("Travel", Request, travel);
            return Message(RedirectToAction("ViewTravel", new { travelID = travel.ID }), "De Locatie is verwijderd");
        }

        /// <summary>
        /// Creates a note and save the corresponding Travelogue. Redirect to the Travelogue detail View.
        /// </summary>
        /// <param name="travelID"></param>
        /// <param name="locationID"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public ActionResult CreateNote(int travelID, int locationID, string note)
        {
            Travel travel = HttpManager.WebserviceGetRequest<Travel>("travel/" + travelID, Request);
            foreach(Location location in travel.Locations) {
                if(location.ID == locationID) {
                    location.Note = note;
                    Travel postedTravel = HttpManager.WebservicePostRequest<Travel>("travel", Request, travel);
                    return Message(RedirectToAction("ViewTravel", "Travel", new { id = travelID }), "Notitie toegevoegd");
                }
            }
            return Error(RedirectToAction("ViewTravel", "Travel", new { id = travelID }), "Er is iets fout gegaan");
        }

        #region Helpers

        /// <summary>
        /// A simple method to request GooglePlaces and converts them to usable LocationDetails
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private List<LocationDetails> RequestGooglePlaces(string url, Dictionary<string, string> parameters)
        {
            GoogleSearchResponse response = HttpManager.GoogleGetRequest<GoogleSearchResponse>(url, parameters);
            if(response != null) {
                List<LocationDetails> locations = new List<LocationDetails>();
                foreach(GoogleResult result in response.Results) {
                    locations.Add(new LocationDetails(result));
                }
                return locations;
            }
            return null;
        }

        #endregion Helpers
    }
}
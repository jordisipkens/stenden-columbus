using ColombusWebapplicatie.Classes.Http;
using ColombusWebapplicatie.Models.Travel.Travelogue;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class TravelogueController : BaseController
    {
        /// <summary>
        /// Gives an overview of all Travelogues.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if(GetCurrentUser() != null) {
                ViewBag.Title = "Mijn Reisverslagen";
                List<Travelogue> travelogues = HttpManager.WebserviceGetRequest<List<Travelogue>>("Travelogue/GetAll", Request, null, new Dictionary<string, string>() { { "userID", GetCurrentUser().ID.ToString() } });
                return View("Index", ShortenTitles(travelogues));
            }
            return Error(RedirectToAction("Index", "Home"), "Er is een fout opgetreden");
        }

        /// <summary>
        /// Displays a list of Travelogues sorted by the given SearchType.
        /// </summary>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        public ActionResult Display(SearchType sortBy)
        {
            ViewBag.Title = "Alle Reisverslagen";
            List<Travelogue> travelogues = HttpManager.WebserviceGetRequest<List<Travelogue>>("Travelogue/Display", Request, null, new Dictionary<string, string>() { { "filter", sortBy.ToString() }, { "limit", "20" } });
            return View("Index", ShortenTitles(travelogues));
        }

        /// <summary>
        /// Opens a existing travelogue and goes to a View to edit the travelogue.
        /// </summary>
        /// <param name="travelogueID"></param>
        /// <returns></returns>
        public ActionResult EditTravelogue(int travelogueID = 0)
        {
            if(travelogueID != 0) {
                Travelogue travelogue = HttpManager.WebserviceGetRequest<Travelogue>(string.Format("Travelogue/{0}", travelogueID), Request);
                if(travelogue != null) {
                    return View("CreateTravelogue", travelogue);
                }
                return Error(RedirectToAction("Index"), "Deze Travelogue bestaat niet (meer)");
            }
            return Error(RedirectToAction("Index", "Travel"), "Er is een fout opgetreden");
        }

        /// <summary>
        /// Creates a new Travelogue to edit
        /// </summary>
        /// <param name="travelID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateTravelogue(int travelID = 0)
        {
            if(travelID != 0) {
                Travelogue travelogue = new Travelogue();
                travelogue.TravelID = travelID;
                Travelogue newTravelogue = HttpManager.WebservicePostRequest<Travelogue>("Travelogue", Request, travelogue);
                if(newTravelogue != null) {
                    return View(newTravelogue);
                }
            }
            return Error(RedirectToAction("Index", "Travel"), "Er is een fout opgetreden");
        }

        /// <summary>
        /// Deletes the given Travelogue and returns to the overview.
        /// </summary>
        /// <param name="travelogueID"></param>
        /// <returns></returns>
        public ActionResult DeleteTravelogue(int travelogueID = 0)
        {
            HttpManager.WebserviceGetRequest<Travelogue>("Travelogue/Delete/" + travelogueID, Request);
            return MessageToIndex("Het reisverslag is verwijderd");
        }

        /// <summary>
        /// View a single Travelogue without editing options,
        /// </summary>
        /// <param name="travelogueID"></param>
        /// <param name="maxTitleLenght"></param>
        /// <returns></returns>
        public ActionResult ViewTravelogue(int travelogueID = 0, int maxTitleLenght = 30)
        {
            if(travelogueID != 0) {
                Travelogue travelogue = HttpManager.WebserviceGetRequest<Travelogue>(string.Format("Travelogue/{0}", travelogueID), Request);
                if(travelogue.Title != null && travelogue.Title.Length >= maxTitleLenght + 20) {
                    travelogue.Title = travelogue.Title.Substring(0, maxTitleLenght + 17) + "...";
                }
                foreach(Paragraph par in travelogue.Paragraphs) {
                    par.AlignImageLeft = (par.ID % 2 == 0);  // Check if the ID is an odd number
                }
                return View(travelogue);
            }
            return Error(RedirectToAction("Index"), "Deze Travelogue bestaat niet (meer)");
        }

        /// <summary>
        /// Checks which button is pressed and handles accordingly.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitButton(Travelogue model)
        {
            if(Request.Form["AddParagraph"] != null) {
                return AddParagraph(model);
            }
            else if(Request.Form["Publish"] != null) {
                PublishTravelogue(model);
                return Index();
            }
            else if(Request.Form["Save"] != null) {
                Travelogue travelogue = HttpManager.WebservicePostRequest<Travelogue>("Travelogue", Request, model);
                return Index();
            }
            return Index();
        }

        /// <summary>
        /// Adds a paragraph to the Travelogue and returns a new View with a new Paraghraph.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ActionResult AddParagraph(Travelogue model)
        {
            if(model.Paragraphs == null) {
                model.Paragraphs = new List<Paragraph>();
            }
            model.Paragraphs.Add(new Paragraph());
            return View("CreateTravelogue", model);
        }

        /// <summary>
        /// Publishes a Travelogue. If the Travelogue is new, it create the Travelogue instead(and
        /// it's already published).
        /// </summary>
        /// <param name="model"></param>
        private void PublishTravelogue(Travelogue model)
        {
            if(model.ID != 0) {
                HttpManager.WebserviceGetRequest<Travelogue>("Travelogue/Publish", Request, null, new Dictionary<string, string>() { { "travelogueID", model.ID.ToString() }, { "isPublic", true.ToString() } });
            }
            else {
                Travelogue savedTravelogue = HttpManager.WebservicePostRequest<Travelogue>("Travelogue", Request, model);
                if(savedTravelogue != null) {
                    PublishTravelogue(savedTravelogue);
                }
            }
        }

        #region Helpers

        /// <summary>
        /// Shortens the titles in case of overflow.
        /// </summary>
        /// <param name="travelogues"></param>
        /// <returns></returns>
        private List<Travelogue> ShortenTitles(List<Travelogue> travelogues, int maxTitleLenght = 30)
        {
            if(travelogues != null) {
                foreach(Travelogue travelogue in travelogues) {
                    if(travelogue.Title != null && travelogue.Title.Length >= maxTitleLenght) {
                        travelogue.Title = travelogue.Title.Substring(0, maxTitleLenght - 3) + "...";
                    }
                }
            }
            return travelogues;
        }

        #endregion Helpers

        /// <summary>
        /// Enum used for Search Filters/Sorting
        /// </summary>
        public enum SearchType
        {
            Best,
            Latest
        }
    }
}
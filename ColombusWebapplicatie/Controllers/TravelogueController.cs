using ColombusWebapplicatie.Classes.Http;
using ColombusWebapplicatie.Models.Travel.Travelogue;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class TravelogueController : BaseController
    {
        private static int maxTitleLenght = 30;

        public ActionResult Index()
        {
            if(GetCurrentUser() != null) {
                ViewBag.Title = "Mijn Reisverslagen";
                List<Travelogue> travelogues = HttpManager.WebserviceGetRequest<List<Travelogue>>("Travelogue/GetAll", Request, null, new Dictionary<string, string>() { { "userID", GetCurrentUser().ID.ToString() } });
                return View("Index", ShortenTitles(travelogues));
            }
            return Error(RedirectToAction("Index", "Home"), "Er is een fout opgetreden");
        }

        public ActionResult Display(SearchType sortBy)
        {
            ViewBag.Title = "Alle Reisverslagen";
            List<Travelogue> travelogues = HttpManager.WebserviceGetRequest<List<Travelogue>>("Travelogue/Display", Request, null, new Dictionary<string, string>() { { "filter", sortBy.ToString() }, { "limit", "20" } });
            return View("Index", ShortenTitles(travelogues));
        }

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

        public ActionResult DeleteTravelogue(int travelogueID = 0)
        {
            throw new NotImplementedException();
        }

        //Method for viewing 1 travelogue
        public ActionResult ViewTravelogue(int travelogueID = 0)
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
                Travelogue travelogue = SaveTravelogue(model);
                return Index();
            }
            return Index();
        }

        private ActionResult AddParagraph(Travelogue model)
        {
            if(model.Paragraphs == null) {
                model.Paragraphs = new List<Paragraph>();
            }
            model.Paragraphs.Add(new Paragraph());
            return View("CreateTravelogue", model);
        }

        private void PublishTravelogue(Travelogue model)
        {
            if(model.ID != 0) {
                HttpManager.WebservicePostRequest<Travelogue>("Travelogue?travelogueId=" + model.ID + "&isPublic=true", Request, model);
            }
            else {
                Travelogue savedTravelogue = SaveTravelogue(model);
                if(savedTravelogue != null) {
                    PublishTravelogue(savedTravelogue);
                }
            }
        }

        private Travelogue SaveTravelogue(Travelogue model)
        {
            return HttpManager.WebservicePostRequest<Travelogue>("Travelogue", Request, model);
        }

        #region Helpers

        private List<Travelogue> ShortenTitles(List<Travelogue> travelogues)
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

        public enum SearchType
        {
            Best,
            Latest
        }
    }
}
using ColombusWebapplicatie.Classes.Http;
using ColombusWebapplicatie.Models.Travel.Travelogue;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class TravelogueController : BaseController
    {
        private static int maxTitleLenght = 30;

        public ActionResult Index()
        {
            Models.User user = Session["User"] as Models.User;
            if(user != null) {
                ViewBag.Title = "Mijn Reisverslagen";
                List<Travelogue> travelogues = HttpManager.WebserviceGetRequest<List<Travelogue>>("Travelogue/GetAll", Request, null, new Dictionary<string, string>() { { "userID", user.ID.ToString() } });
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
                SaveTravelogue(model);
                return Index();
            }
            return Index();
        }

        //Method for viewing 1 travelogue
        public ActionResult ViewTravelogue(int? id)
        {
            Travelogue model = new Travelogue();
            // Load Json file.
            StreamReader streamReader = new StreamReader(Server.MapPath("~/Content/json/Travelogue.json"));
            // Deserialize Json to list of Travel objects.
            model = JsonConvert.DeserializeObject<Travelogue>(streamReader.ReadToEnd());
            //Travelogue model = HTTPManager.WebserviceGetRequest<Travelogue>("Travelogue/" + id, Request);
            if(model.Title.Length >= maxTitleLenght + 20) {
                model.Title = model.Title.Substring(0, maxTitleLenght + 17) + "...";
            }
            foreach(Paragraph par in model.Paragraphs) {
                par.AlignImageLeft = (par.ID % 2 == 0);  // Check if the ID is an odd number
            }
            return View(model);
        }

        private ActionResult AddParagraph(Travelogue model)
        {
            if(model.Paragraphs != null) {
                model.Paragraphs.Add(new Paragraph());
            }
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
                    if(travelogue.Title.Length >= maxTitleLenght) {
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
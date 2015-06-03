using ColombusWebapplicatie.Classes.Http;
using ColombusWebapplicatie.Models.Travel;
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
            List<Travelogue> model = new List<Travelogue>();
            // Load Json file.
            StreamReader streamReader = new StreamReader(Server.MapPath("~/Content/json/Travelogue.json"));
            // Deserialize Json to list of Travel objects.
            model.Add(JsonConvert.DeserializeObject<Travelogue>(streamReader.ReadToEnd()));
            foreach (Travelogue travelogue in model)
            {
                if (travelogue.Title.Length >= maxTitleLenght)
                {
                    travelogue.Title = travelogue.Title.Substring(0, maxTitleLenght - 3) + "...";
                }
            }

            return View("Index", model);
        }

        public ActionResult CreateTravelogue()
        {
            Travelogue model = new Travelogue();
            model.Paragraphs = new List<Paragraph>();
            model.Paragraphs.Add(new Paragraph());
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateTravelogue(Travel travel)
        {
            Travelogue model = new Travelogue();
            model.Paragraphs = new List<Paragraph>();
            model.Paragraphs.Add(new Paragraph());
            model.Travel = travel;
            model.TravelID = travel.ID;
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubmitButton(Travelogue model)
        {
            if (Request.Form["AddParagraph"] != null)
            {
                return AddParagraph(model);
            }
            else if (Request.Form["Publish"] != null)
            {
                PublishTravelogue(model);
                return Index();
            }
            else if (Request.Form["Save"] != null)
            {
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
            if (model.Title.Length >= maxTitleLenght + 20)
            {
                model.Title = model.Title.Substring(0, maxTitleLenght + 17) + "...";
            }
            foreach (Paragraph par in model.Paragraphs)
            {
                par.AlignImageLeft = (par.ID % 2 == 0);  // Check if the ID is an odd number
            }
            return View(model);
        }
        private ActionResult AddParagraph(Travelogue model)
        {
            if (model.Paragraphs != null)
            {
                model.Paragraphs.Add(new Paragraph());
            }
            return View("CreateTravelogue", model);
        }
        private void PublishTravelogue(Travelogue model)
        {
            if (model.ID != 0)
            {
                HTTPManager.WebservicePostRequest<Travelogue>("Travelogue?travelogueId=" + model.ID + "&isPublic=true", Request, model);
            }
            else
            {
                Travelogue savedTravelogue = SaveTravelogue(model);
                if (savedTravelogue != null)
                {
                    PublishTravelogue(savedTravelogue);
                }
            }
        }

        private Travelogue SaveTravelogue(Travelogue model)
        {
            return HTTPManager.WebservicePostRequest<Travelogue>("Travelogue", Request, model);
        }
    }
}
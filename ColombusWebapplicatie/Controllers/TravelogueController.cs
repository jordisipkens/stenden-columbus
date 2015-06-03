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

        public ActionResult Index()
        {
            List<Travelogue> model = new List<Travelogue>();
            // Load Json file.
            StreamReader streamReader = new StreamReader(Server.MapPath("~/Content/json/Travelogue.json"));
            // Deserialize Json to list of Travel objects.
            model.Add(JsonConvert.DeserializeObject<Travelogue>(streamReader.ReadToEnd()));

            return View("Index", model);
        }

        //Method for viewing 1 travelogue
        public ActionResult ViewTravelogue(int? id)
        {
            Travelogue model = HTTPManager.WebserviceGetRequest<Travelogue>("Travelogue/" + id, Request);
            foreach(Paragraph par in model.Paragraphs) {
                par.AlignImageLeft = (par.ID % 2 == 0);  // Check if the ID is an odd number
            }
            return View(model);
        }

        public ActionResult CreateTravelogue()
        {
            Travelogue model = new Travelogue();
            model.Paragraphs = new List<Paragraph>();
            model.Paragraphs.Add(new Paragraph());
            return View(model);
        }

        private ActionResult AddParagraph(Travelogue model)
        {
            if(model.Paragraphs != null) {
                model.Paragraphs.Add(new Paragraph());
            }
            return View("CreateTravelogue", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubmitButton(Travelogue model)
        {
            if(Request.Form["AddParagraph"] != null) {
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

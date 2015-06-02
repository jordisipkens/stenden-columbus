using ColombusWebapplicatie.Classes;
using ColombusWebapplicatie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ColombusWebapplicatie.Classes;

namespace ColombusWebapplicatie.Controllers
{
    public class TravelogueController : BaseController
    {
        #region dummydata
        //
        // GET: /Travelogue/

        ////Dummy data
        //model.Author = new User { FirstName = "Jarno", LastName = "Botke", Email = "420_YOLOYLOO@hotmale.com", ID = 1 };
        //model.StartDate = DateTime.Now;
        //model.EndDate = DateTime.Now.AddDays(10);
        //model.ID = 1;
        //model.Header = "Vakantie in Limburg 2015";
        //model.Locations = new List<Location> { new Location { Date = DateTime.Now, ID = 0, LocationDetails = new LocationDetails { Address = "Aleemannekeweg 23", ID = 0, Coordinates = new Coordinates { Latitude = 50.00, Longitude = 50.00 }, Name = "Museum Het Doek", PhoneNumber = "0683838383", PlaceID = "1A" }, Note = "Vandaag heerlijk gewandeld!" }, new Location { Date = DateTime.Now, ID = 0, LocationDetails = new LocationDetails { Address = "Aleemannekeweg 55", ID = 0, Coordinates = new Coordinates { Latitude = 51.00, Longitude = 51.00 }, Name = "Kinderboerderij de Egel", PhoneNumber = "077636363663", PlaceID = "2A" }, Note = "Vandaag nog heerlijker geschept!" } };
        //model.Paragraphs = new List<Paragraph> { 
        //    new Paragraph { 
        //        AlignImageLeft = true,
        //        ImagePath = "http://d2hv3zvds9z8pu.cloudfront.net/img/112367_320x240_4890.jpg",
        //        Text = "EERSTE ALINEA ALIGN LEFT TRUE  Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras eget turpis eros. Vivamus consectetur molestie metus, a maximus tellus rutrum eu. Ut vehicula tortor et convallis egestas. Phasellus sit amet nulla nec leo dapibus iaculis. Proin elementum at mi id elementum. Maecenas facilisis lorem nec mauris consequat consequat. Donec cursus commodo ante. Morbi vitae interdum mauris. Phasellus tempus turpis ut quam congue auctor.In posuere posuere ligula. In consequat, risus id aliquam laoreet, erat massa mollis massa, vitae viverra lectus augue non nisi. Praesent dapibus bibendum placerat. Etiam ut suscipit lectus. Maecenas blandit felis ipsum, non malesuada mi elementum et. Sed vel bibendum ipsum, auctor dignissim felis. Aenean rhoncus elit ac faucibus pellentesque. Nullam a leo vitae tortor aliquam accumsan et a justo. Mauris nulla turpis, pharetra quis dictum ac, tincidunt posuere enim. Etiam nec lectus sed nisl ornare placerat ac id metus. Cras eu efficitur orci, vitae sagittis mi. Fusce id consectetur nulla. In in congue turpis." 
        //    }, 
        //    new Paragraph {
        //        AlignImageLeft = false,
        //        ImagePath = "http://d2hv3zvds9z8pu.cloudfront.net/img/112367_320x240_4890.jpg",
        //        Text = "TWEEDE ALINEA ALIGN LEFT FALSE  Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras eget turpis eros. Vivamus consectetur molestie metus, a maximus tellus rutrum eu. Ut vehicula tortor et convallis egestas. Phasellus sit amet nulla nec leo dapibus iaculis. Proin elementum at mi id elementum. Maecenas facilisis lorem nec mauris consequat consequat. Donec cursus commodo ante. Morbi vitae interdum mauris. Phasellus tempus turpis ut quam congue auctor.In posuere posuere ligula. In consequat, risus id aliquam laoreet, erat massa mollis massa, vitae viverra lectus augue non nisi. Praesent dapibus bibendum placerat. Etiam ut suscipit lectus. Maecenas blandit felis ipsum, non malesuada mi elementum et. Sed vel bibendum ipsum, auctor dignissim felis. Aenean rhoncus elit ac faucibus pellentesque. Nullam a leo vitae tortor aliquam accumsan et a justo. Mauris nulla turpis, pharetra quis dictum ac, tincidunt posuere enim. Etiam nec lectus sed nisl ornare placerat ac id metus. Cras eu efficitur orci, vitae sagittis mi. Fusce id consectetur nulla. In in congue turpis." 
        //    },
        //    new Paragraph {
        //        FullpageImage = true,
        //        ImagePath = "http://d2hv3zvds9z8pu.cloudfront.net/img/112367_320x240_4890.jpg",
        //    }
        //};
#endregion

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

            Travelogue model = HTTPManager.GetRequest<Travelogue>("Travelogue/" + id, Request);
            foreach (Paragraph par in model.Paragraphs)
            {
                par.AlignImageLeft = (par.ID % 2 == 0);  // Check if the ID is an odd number
            }
            return View(model);
        }
       
        public ActionResult CreateTravelogue()
        {
            ColombusWebapplicatie.Models.Travelogue model = new Travelogue();
            model.Paragraphs = new List<Paragraph>();
            model.Paragraphs.Add(new Paragraph());
            return View(model);
        }

        public ActionResult AddParagraph(Travelogue model)
        {
            if (model.Paragraphs != null)
            {
                model.Paragraphs.Add(new Paragraph());
        }
            return View("CreateTravelogue", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubmitButton(Travelogue model)
        {
            if (Request.Form["AddParagraph"] != null)
            {
                return AddParagraph(model);
        }
            else if (Request.Form["Index"] != null)
        {
                return Index();
            }
            return Index();
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using ColombusWebapplicatie.Models;

namespace ColombusWebapplicatie.Controllers
{
    public class TravelController : Controller
    {
        //
        // GET: /Travel/

        // View all travels.
        public ActionResult Index()
        {
            // Load Json file.
            StreamReader streamReader = new StreamReader(Server.MapPath("~/Content/json/Travel.json"));
            // Deserialize Json to list of Travel objects.
            List<Travel> travels = JsonConvert.DeserializeObject<List<Travel>>(streamReader.ReadToEnd());
            return View(travels);
        }

        // View one travel.
        public ActionResult ViewTravel(int id)
        {
            return View();
        }

    }
}

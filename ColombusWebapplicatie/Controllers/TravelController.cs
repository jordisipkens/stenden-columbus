using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using ColombusWebapplicatie.Models;
using System.Globalization;

namespace ColombusWebapplicatie.Controllers
{
    public class TravelController : BaseController
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
            // Return the regarding view.
            return View(travels);
        }

        public ActionResult ViewTravel(int? id)
        {
            // !!! Important: Not finished, just for testing !!! Supposed to check if the travel with the regarding id exists.
            if (id != null) {
                // Return to the index of the controller with an error message.
                return ErrorToIndex("Deze reis bestaat niet (meer).");
            } else {
                // Load Json file.
                StreamReader streamReader = new StreamReader(Server.MapPath("~/Content/json/Travel.json"));
                // Deserialize Json to list of Travel objects.
                List<Travel> travels = JsonConvert.DeserializeObject<List<Travel>>(streamReader.ReadToEnd());
                // !!! Important: Not finished, just for testing !!! Supposed to get the travel with the regarding id.
                Travel travel = travels.First();
                // Return the regarding view.
                return View(travel);
            }
        }

    }
}

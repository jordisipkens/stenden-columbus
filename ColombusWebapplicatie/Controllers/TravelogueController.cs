﻿using ColombusWebapplicatie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class TravelogueController : BaseController
    {
        //
        // GET: /Travelogue/

        public ActionResult Index()
        {
            Travelogue model = new Travelogue();

            //Dummy data
            model.Author = new User { FirstName = "Jarno", LastName = "Botke", Email = "420_YOLOYLOO@hotmale.com", ID = 1 };
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now.AddDays(10);
            model.ID = 1;
            model.Title = "Stront scheppen in Limburg 2015";
            model.Locations = new List<Location> { new Location { Date = DateTime.Now, ID = 0, LocationDetails = new LocationDetails { Address = "Aleemannekeweg 23", ID = 0, Coordinates = new Coordinates { Latitude = 50.00, Longitude = 50.00 }, Name = "Kinderboerderij de Lokker", PhoneNumber = "0683838383", PlaceID = "1A" }, Note = "Vandaag heerlijk geschept!" } };
            model.Locations = new List<Location> { new Location { Date = DateTime.Now, ID = 0, LocationDetails = new LocationDetails { Address = "Aleemannekeweg 55", ID = 0, Coordinates = new Coordinates { Latitude = 51.00, Longitude = 51.00 }, Name = "Kinderboerderij de Vleesfabriek", PhoneNumber = "077636363663", PlaceID = "2A" }, Note = "Vandaag nog heerlijker geschept!" } };


            return View(model);
        }

    }
}

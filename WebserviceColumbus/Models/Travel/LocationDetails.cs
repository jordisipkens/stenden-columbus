﻿namespace WebserviceColumbus.Models.Travel
{
    public class LocationDetails
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Adres { get; set; }
        public string PhoneNumber { get; set; }
        public string PlaceID { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}

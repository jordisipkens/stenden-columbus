using System;

namespace WebserviceColumbus.Models.Travel
{
    public class Location
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public LocationDetails LocationDetails { get; set; }
    }
}

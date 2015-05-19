using System;
using System.Collections.Generic;

namespace WebserviceColumbus.Models.Travel
{
    public class Travel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Location> Locations { get; set; }
        public User User { get; set; }
    }
}
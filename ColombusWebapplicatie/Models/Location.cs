using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColombusWebapplicatie.Models
{
    public class Location
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public LocationDetails LocationDetails { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColombusWebapplicatie.Models
{
    public class LocationDetails
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PlaceID { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}
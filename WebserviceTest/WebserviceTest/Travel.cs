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

        public virtual ICollection<Location> Locations { get; set; }

        public int UserID { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ColombusWebapplicatie.Models.Google;

namespace ColombusWebapplicatie.Models
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
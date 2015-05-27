using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ColombusWebapplicatie.Models
{
    public class Travelogue
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Location> Locations { get; set; }
        public User Author { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
    }
}
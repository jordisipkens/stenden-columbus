using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ColombusWebapplicatie.Models
{
    public class Travelogue
    {
        //public int ID { get; set; }
        //public int TravelID { get; set; }
        //public string Header { get; set; }
        //public List<Paragraph> Paragraphs { get; set; }

        [Key]
        public int ID { get; set; }

        [Required]
        public int TravelID { get; set; }

        public string Title { get; set; }

        public bool Published { get; set; }

        public DateTime PublishedTime { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Paragraph> Paragraphs { get; set; }
    }
}
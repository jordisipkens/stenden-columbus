using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebserviceColumbus.Model;

namespace WebserviceColumbus.Models.Travel.Travelogue
{
    public class Travelogue : iDbEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int TravelID { get; set; }

        public bool Published { get; set; }

        public DateTime PublishedTime { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Paragraph> Paragraphs { get; set; }
    }
}
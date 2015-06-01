using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ColombusWebapplicatie.Models
{
    public class Rating
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public double RatingValue { get; set; }

        //Navigation
        [JsonIgnore]
        public virtual Travelogue Travelogue { get; set; }
    }
}
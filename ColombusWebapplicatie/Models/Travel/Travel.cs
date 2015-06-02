using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ColombusWebapplicatie.Models.Travel
{
    public class Travel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IList<Location> Locations { get; set; }

        [Required]
        public User User { get; set; }
    }
}
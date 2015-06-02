using ColombusWebapplicatie.Models.Google;
using System;
using System.ComponentModel.DataAnnotations;

namespace ColombusWebapplicatie.Models.Travel
{
    public class Location
    {
        [Key]
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        [Required]
        public LocationDetails LocationDetails { get; set; }

        public void SetLocationDetails(GoogleResult result)
        {
            LocationDetails = new LocationDetails(result);
        }
    }
}
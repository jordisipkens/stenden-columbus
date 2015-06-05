using ColombusWebapplicatie.Models.Google;
using ColombusWebapplicatie.Models.Google.Details;
using ColombusWebapplicatie.Models.Google.Search;
using System;
using System.ComponentModel.DataAnnotations;

namespace ColombusWebapplicatie.Models.Travel
{
    public class Location
    {
        public Location() { }

        public Location(GooglePlace place, DateTime date)
        {
            this.Date = date;
            this.LocationDetails = new LocationDetails(place);
        }

        [Key]
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        [Required]
        public LocationDetails LocationDetails { get; set; }

        public int LocationDetailsID { get; set; }
    }
}
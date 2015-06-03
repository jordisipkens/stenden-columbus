using ColombusWebapplicatie.Models.Google;
using System;
using System.ComponentModel.DataAnnotations;

namespace ColombusWebapplicatie.Models.Travel
{
    public class LocationDetails
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string PlaceID { get; set; }

        [Required]
        public Coordinates Coordinates { get; set; }

        public LocationDetails() { }

        public LocationDetails(GoogleResult result)
        {
            Name = result.Name;
            //PhoneNumber = result.PhoneNumber;
            PlaceID = result.PlaceID;
            Address = result.Vicinity;
            Coordinates = new Coordinates() {
                Latitude = Convert.ToDouble(result.Geometry.Location.Latitude),
                Longitude = Convert.ToDouble(result.Geometry.Location.Longitude)
            };
        }
    }
}
using ColombusWebapplicatie.Models.Google.Details;
using ColombusWebapplicatie.Models.Google.Search;
using System;
using System.ComponentModel.DataAnnotations;

namespace ColombusWebapplicatie.Models.Travel
{
    public class LocationDetails
    {
        public LocationDetails()
        {
        }

        public LocationDetails(GoogleResult result)
        {
            this.Name = result.Name;
            this.PlaceID = result.PlaceID;
            this.Address = result.Vicinity;
            this.Coordinates = new Coordinates() {
                Latitude = Convert.ToDouble(result.Geometry.Location.Latitude),
                Longitude = Convert.ToDouble(result.Geometry.Location.Longitude)
            };
        }

        public LocationDetails(GooglePlace place)
        {
            this.Name = place.Name;
            this.Address = place.FormattedAddress;
            this.PhoneNumber = place.InternationalPhoneNumber;
            this.PlaceID = place.PlaceID;
            this.Coordinates = new Coordinates() {
                Latitude = Convert.ToDouble(place.Geometry.Location.Latitude),
                Longitude = Convert.ToDouble(place.Geometry.Location.Longitude)
            };
        }

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

        public int CoordinatesID { get; set; }
    }
}
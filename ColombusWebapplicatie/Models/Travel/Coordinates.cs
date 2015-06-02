using System.ComponentModel.DataAnnotations;

namespace ColombusWebapplicatie.Models.Travel
{
    public class Coordinates
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
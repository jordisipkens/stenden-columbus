using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ColombusWebapplicatie.Models.Travel.Travelogue
{
    public class Rating
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public double RatingValue { get; set; }
    }
}
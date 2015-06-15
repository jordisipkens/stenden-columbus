using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebserviceColumbus.Models.Travel.Travelogue
{
    public class Rating
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public double RatingValue { get; set; }

        [Required]
        public int userID { get; set; }

        //Navigation
        [JsonIgnore]
        public virtual Travelogue Travelogue { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebserviceColumbus.Database;

namespace WebserviceColumbus.Models.Travel.Travelogue
{
    public class Travelogue : iDbEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int TravelID { get; set; }

        public ICollection<Paragraph> Paragraphs { get; set; }
    }
}
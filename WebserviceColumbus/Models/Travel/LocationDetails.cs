using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebserviceColumbus.Database;

namespace WebserviceColumbus.Models.Travel
{
    public class LocationDetails : iDbEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Adres { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string PlaceID { get; set; }

        [ForeignKey("CoordinatesID")]
        public virtual Coordinates Coordinates { get; set; }

        [Required]
        public int CoordinatesID { get; set; }
    }
}
using WebserviceColumbus.Database;
namespace WebserviceColumbus.Models
{
    public class Photo : iDbEntity
    {
        public int ID { get; set; }
        public string Caption { get; set; }
        public string URL { get; set; }
        public int LocationID { get; set; }
        public int TravelID { get; set; }
    }
}
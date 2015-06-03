namespace ColombusWebapplicatie.Models
{
    public class Photo
    {
        public int ID { get; set; }

        public string Caption { get; set; }

        public string URL { get; set; }

        public int LocationID { get; set; }

        public int TravelID { get; set; }
    }
}
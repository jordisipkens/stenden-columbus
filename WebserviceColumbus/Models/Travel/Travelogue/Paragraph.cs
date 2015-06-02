using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebserviceColumbus.Models.Travel.Travelogue
{
    public class Paragraph
    {
        [Key]
        public int ID { get; set; }

        public string Header { get; set; }

        public string Text { get; set; }

        public int PhotoID { get; set; }

        public bool FullWidth { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
        
        public bool AlignImageLeft { get; set; }

        //Navigation
        [JsonIgnore]
        public virtual Travelogue Travelogue { get; set; }
    }
}

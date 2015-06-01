using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ColombusWebapplicatie.Models
{
    public class Paragraph
    {
        //public int ID { get; set; }
        //public string Header { get; set; }
        //public string Text { get; set; }

        //public int PhotoID { get; set; }
        //public bool FullWidth { get; set; }
        //public int Width { get; set; }
        //public int Height { get; set; }
        //public string ImagePath { get; set; }
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
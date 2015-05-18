using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebserviceColumbus.Models.Travel
{
    public class Paragraph
    {
        public int ID { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public int PhotoID { get; set; }
        public bool FullWidth { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

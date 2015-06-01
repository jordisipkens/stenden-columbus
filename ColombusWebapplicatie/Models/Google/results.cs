using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColombusWebapplicatie.Models.Google
{
    public class results
    {
        public geometry geometry { get; set; }
        public string name { get; set; }
        public string reference { get; set; }
        public string vicinity { get; set; }
    }
}
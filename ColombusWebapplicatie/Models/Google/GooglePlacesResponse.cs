using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColombusWebapplicatie.Models.Google
{
    public class GooglePlacesResponse
    {
        public string[] html_attributes  { get; set; }
        public results[] results { get; set; }
        public string status { get; set; }

    }
}
using Newtonsoft.Json;
using System;

namespace WebserviceColumbus.Models.Travel
{
    public class Location
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public virtual LocationDetails LocationDetails { get; set; }

        public int LocationDetailsID { get; set; }

        //Navigation
        [JsonIgnore]
        public virtual Travel Travel { get; set; }
    }
}
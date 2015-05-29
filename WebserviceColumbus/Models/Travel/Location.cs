using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebserviceColumbus.Model;

namespace WebserviceColumbus.Models.Travel
{
    public class Location : iDbEntity
    {
        [Key]
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        [ForeignKey("LocationDetailsID")]
        public virtual LocationDetails LocationDetails { get; set; }

        [Required, JsonIgnore]
        public int LocationDetailsID { get; set; }

        //Navigation
        [JsonIgnore]
        public virtual Travel Travel { get; set; }
    }
}
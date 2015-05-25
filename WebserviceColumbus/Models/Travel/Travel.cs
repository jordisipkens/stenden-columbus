using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebserviceColumbus.Database;
using WebserviceColumbus.Models.Other;

namespace WebserviceColumbus.Models.Travel
{
    public class Travel : iDbEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual ICollection<Location> Locations { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [Required, JsonIgnore]
        public int UserID { get; set; }
    }
}
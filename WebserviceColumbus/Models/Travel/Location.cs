﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebserviceColumbus.Database;

namespace WebserviceColumbus.Models.Travel
{
    public class Location : iDbEntity
    {
        [Key]
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        [ForeignKey("LocationDetailsID")]
        public LocationDetails LocationDetails { get; set; }
        [Required]
        public int LocationDetailsID { get; set; }

        //Navigation
        public virtual Travel Travel { get; set; }
    }
}
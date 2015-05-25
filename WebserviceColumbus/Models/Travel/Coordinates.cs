﻿using System.ComponentModel.DataAnnotations;
using WebserviceColumbus.Database;

namespace WebserviceColumbus.Models.Travel
{
    public class Coordinates : iDbEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;

namespace WebserviceColumbus.Models.Travel.Travelogue
{
    public class Travelogue
    {
        //[Key]
        public int ID { get; set; }

        //[Required]
        public int TravelID { get; set; }

        public string Title { get; set; }

        public bool Published { get; set; }

        public DateTime PublishedTime { get; set; }

        public ICollection<Paragraph> Paragraphs { get; set; }
    }
}
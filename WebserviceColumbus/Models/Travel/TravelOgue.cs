﻿using System.Collections.Generic;
namespace WebserviceColumbus.Models.Travel
{
    public class TravelOgue
    {
        public int ID { get; set; }
        public int TravelID { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
    }
}
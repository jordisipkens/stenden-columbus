﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ColombusWebapplicatie.Models.Travel.Travelogue
{
    public class Travelogue
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int TravelID { get; set; }

        public string Title { get; set; }

        public bool Published { get; set; }

        public DateTime PublishedTime { get; set; }

        public virtual IList<Rating> Ratings { get; set; }

        public double AvarageRating
        {
            get
            {
                if(Ratings != null && Ratings.Count > 0) {
                    double total = 0;
                    foreach(Rating rating in Ratings) {
                        total += rating.RatingValue;
                    }
                    return total / Ratings.Count;
                }
                return 0;
            }
        }

        public virtual IList<Paragraph> Paragraphs { get; set; }

        [JsonIgnore]
        public int Index { get; set; }

        [JsonIgnore]
        public Travel Travel { get; set; }

        public Travelogue()
        {
            PublishedTime = new DateTime(1970, 1, 1);
        }
    }
}
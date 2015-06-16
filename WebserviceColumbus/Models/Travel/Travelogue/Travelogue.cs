using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebserviceColumbus.Model;

namespace WebserviceColumbus.Models.Travel.Travelogue
{
    public class Travelogue : iDbEntity
    {
        private DateTime publishedTime;

        [Key]
        public int ID { get; set; }

        [Required]
        public int TravelID { get; set; }

        public string Title { get; set; }

        public bool Published { get; set; }

        public DateTime PublishedTime
        {
            get
            {
                if(publishedTime == null) {
                    publishedTime = DateTime.Now;
                }
                return publishedTime;
            }
            set
            {
                publishedTime = value;
            }
        }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Paragraph> Paragraphs { get; set; }

        [NotMapped]
        public string Author { get; set; }

        [NotMapped, JsonIgnore]
        public double RatingFactor { get; set; }

        [NotMapped, JsonIgnore]
        public double TotalRating
        {
            get
            {
                double total = 0;
                if(Ratings != null) {
                    foreach(Rating rating in Ratings) {
                        total += rating.RatingValue;
                    }
                }
                return total;
            }
        }
    }
}

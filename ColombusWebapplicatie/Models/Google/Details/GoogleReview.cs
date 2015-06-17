using Newtonsoft.Json;

namespace ColombusWebapplicatie.Models.Google.Details
{
    public class GoogleReview
    {
        [JsonProperty("aspects")]
        public GoogleReviewAspect[] Aspects { get; set; }

        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        [JsonProperty("author_url")]
        public string AuthorUrl { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }
    }
}
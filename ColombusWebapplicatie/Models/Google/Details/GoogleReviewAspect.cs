using Newtonsoft.Json;

namespace ColombusWebapplicatie.Models.Google.Details
{
    public class GoogleReviewAspect
    {
        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
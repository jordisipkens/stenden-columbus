using Newtonsoft.Json;

namespace ColombusWebapplicatie.Models.Google.Search
{
    public class GoogleSearchResponse
    {
        [JsonProperty("html_attributes")]
        public string[] HtmlAttributes { get; set; }

        [JsonProperty("results")]
        public GoogleResult[] Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
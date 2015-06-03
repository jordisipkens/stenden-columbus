using Newtonsoft.Json;

namespace ColombusWebapplicatie.Models.Google
{
    public class GoogleResponse
    {
        [JsonProperty("html_attributes")]
        public string[] HtmlAttributes { get; set; }

        [JsonProperty("results")]
        public GoogleResult[] Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
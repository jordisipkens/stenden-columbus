using Newtonsoft.Json;

namespace ColombusWebapplicatie.Models.Google.Details
{
    public class GoogleDetailResponse
    {
        [JsonProperty("html_attributions")]
        public object[] HtmlAttributions { get; set; }

        [JsonProperty("result")]
        public GooglePlace Result { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
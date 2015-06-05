using Newtonsoft.Json;

namespace ColombusWebapplicatie.Models.Google.Details
{
    public class GoogleAddressComponent
    {
        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("long_name")]
        public string LongName { get; set; }

        [JsonProperty("types")]
        public string[] Types { get; set; }
    }
}
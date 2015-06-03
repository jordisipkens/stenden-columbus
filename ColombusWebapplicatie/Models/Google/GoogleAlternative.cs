using Newtonsoft.Json;

namespace ColombusWebapplicatie.Models.Google
{
    public class GoogleAlternative
    {
        [JsonProperty("PlaceID")]
        public string place_id { get; set; }

        [JsonProperty("Scope")]
        public string scope { get; set; }
    }
}
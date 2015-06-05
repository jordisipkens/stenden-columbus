using Newtonsoft.Json;

namespace ColombusWebapplicatie.Models.Google
{
    public class GoogleAlternative
    {
        [JsonProperty("place_id")]
        public string PlaceID { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}
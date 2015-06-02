using Newtonsoft.Json;

namespace ColombusWebapplicatie.Models.Google
{
    public class GoogleLocation
    {
        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("lng")]
        public string Longitude { get; set; }
    }
}
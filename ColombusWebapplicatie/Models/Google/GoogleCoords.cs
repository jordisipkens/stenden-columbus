using Newtonsoft.Json;

namespace ColombusWebapplicatie.Models.Google.Search
{
    public class GoogleCoords
    {
        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("lng")]
        public string Longitude { get; set; }
    }
}
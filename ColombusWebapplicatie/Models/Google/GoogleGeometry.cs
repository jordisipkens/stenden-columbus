using ColombusWebapplicatie.Models.Google.Search;
using Newtonsoft.Json;

namespace ColombusWebapplicatie.Models.Google
{
    public class GoogleGeometry
    {
        [JsonProperty("location")]
        public GoogleCoords Location { get; set; }
    }
}
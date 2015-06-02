using Newtonsoft.Json;

namespace ColombusWebapplicatie.Models.Google
{
    public class GoogleGeometry
    {
        [JsonProperty("location")]
        public GoogleLocation Location { get; set; }
    }
}
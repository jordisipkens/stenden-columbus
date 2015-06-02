using Newtonsoft.Json;

namespace ColombusWebapplicatie.Models.Google
{
    public class GoogleResult
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("geometry")]
        public GoogleGeometry Geometry { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("open_now")]
        public bool IsOpen { get; set; }

        [JsonProperty("photos")]
        public GooglePhoto[] Photos { get; set; }

        [JsonProperty("place_id")]
        public string PlaceID { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("alt_ids")]
        public GoogleAlternative[] AlternativeIDs { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("types")]
        public string[] Types { get; set; }

        [JsonProperty("vicinity")]
        public string Vicinity { get; set; }
    }
}
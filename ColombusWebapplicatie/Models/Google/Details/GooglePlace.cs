using Newtonsoft.Json;
using System;
namespace ColombusWebapplicatie.Models.Google.Details
{
    public class GooglePlace
    {
        [JsonProperty("address_components")]
        public GoogleAddressComponent[] AddressComponents { get; set; }

        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("formatted_phone_number")]
        public string FormattedPhoneNumber { get; set; }

        [JsonProperty("geometry")]
        public GoogleGeometry Geometry { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("international_phone_number")]
        public string InternationalPhoneNumber { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("place_id")]
        public string PlaceID { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("alt_ids")]
        public GoogleAlternative[] AltIDs { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("reviews")]
        public object[] Reviews { get; set; }

        [JsonProperty("types")]
        public string[] Types { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("vicinity")]
        public string Vicinity { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        public DateTime Date { get; set; }
    }
}
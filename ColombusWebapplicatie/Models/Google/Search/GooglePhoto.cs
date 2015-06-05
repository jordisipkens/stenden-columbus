using Newtonsoft.Json;

namespace ColombusWebapplicatie.Models.Google.Search
{
    public class GooglePhoto
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("html_attributes")]
        public object[] HtmlAttributes { get; set; }

        [JsonProperty("photo_reference")]
        public string PhotoReference { get; set; }
    }
}
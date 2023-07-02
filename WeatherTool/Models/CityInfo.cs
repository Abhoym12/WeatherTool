using Newtonsoft.Json;

namespace WeatherTool.Models
{
    public class CityInfo
    {
        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lng")]
        public double Longitude { get; set; }
    }
}

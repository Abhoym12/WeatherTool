using Newtonsoft.Json;

namespace WeatherTool.Models
{
    public class CurrentWeather
    {
        [JsonProperty("temperature")]
        public decimal Temperature { get; set; }

        [JsonProperty("windspeed")]
        public decimal WindSpeed { get; set; }

        [JsonProperty("winddirection")]
        public decimal WindDirection { get; set; }

        [JsonProperty("weathercode")]
        public int WeatherCode { get; set; }

        [JsonProperty("is_day")]
        public int IsDay { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }
    }
}

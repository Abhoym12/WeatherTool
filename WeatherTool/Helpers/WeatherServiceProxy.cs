using Newtonsoft.Json;
using WeatherTool.Models;

namespace WeatherTool.Helpers
{
    /// <summary>
    /// Proxy class to call API to fetch Weather related data
    /// </summary>
    public class WeatherServiceProxy: IWeatherServiceProxy
    {
        private readonly HttpClient _httpClient;
        public WeatherServiceProxy()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.open-meteo.com")
            };
        }

        /// <summary>
        /// Method to fetch Weather related data from API
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>Weather Data</returns>
        public async Task<WeatherData?> GetWeatherData(double latitude, double longitude)
        {
            var serviceUrl = $"/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true";
            var response = await _httpClient.GetAsync(serviceUrl);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                return null;
            }
            var weatherData = JsonConvert.DeserializeObject<WeatherData>(response.Content.ReadAsStringAsync().Result);
            return weatherData;
        }
    }
}

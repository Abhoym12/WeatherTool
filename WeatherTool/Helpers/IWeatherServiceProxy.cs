using WeatherTool.Models;

namespace WeatherTool.Helpers
{
   public interface IWeatherServiceProxy
    {
        Task<WeatherData?> GetWeatherData(double latitude, double longitude);
    } 
}

using Newtonsoft.Json;
using WeatherTool.Helpers;
using WeatherTool.Models;

namespace WeatherTool
{
    public class WeatherDetails
    {
        private readonly IWeatherServiceProxy _weatherServiceProxy;
        public WeatherDetails(IWeatherServiceProxy weatherServiceProxy)
        {
            _weatherServiceProxy = weatherServiceProxy;
        }

        /// <summary>
        /// Main driver function that handles console input/output and calls a proxy class to fetch data from API.
        /// </summary>
        public async Task GetWeatherDetails()
        {
            //Loading data from CoordinateDetails.json to a c# variable
            var citiesData = ReadCityCoordinateData("Data/CoordinateDetails.json"); 
            if (!citiesData.Any())
            {
                Console.WriteLine("Error processing the request. Please enter any key to exit.");                
                Console.ReadLine();
                return;
            }

            var welcomeMessage = $@"
            +-------------------------------------------+
                    Welcome to the Weather Tool
            +-------------------------------------------+";
            Console.WriteLine(welcomeMessage);
            CityInfo? cityInfo = null;
            string? city = string.Empty;

            //Reading the city name from user and fetching the matching city info.
            //Using do while loop so that user can enter city name again if wrong value is entered.
            do
            {
                Console.WriteLine("Enter the city name: ");
                city = Console.ReadLine();
                cityInfo = citiesData.FirstOrDefault(o=>o.City?.ToLower() == city?.ToLower());
                if (cityInfo == null)
                {
                    Console.WriteLine("Please enter correct city name."); 
                }
            } while (cityInfo == null);
            
            //Fetching the weather data from API using proxy class
            var weatherData = await _weatherServiceProxy.GetWeatherData(cityInfo.Latitude,cityInfo.Longitude);

            if (weatherData == null || weatherData.CurrentWeather == null)
            {
                Console.WriteLine("Error fetching weather data. Please enter any key to exit.");                
                Console.ReadKey();
                return;
            }

            var output = $@"Weather details for {city}:
            +--------------------------------------------------------------------------------------------------------------------+
            | {weatherData.TimezoneAbbreviation} Time : {weatherData.CurrentWeather.Time.ToString("hh:mm:ss tt")} | Temperature: {weatherData.CurrentWeather.Temperature}°C | Wind Direction: {weatherData.CurrentWeather.WindDirection}° | Wind Speed: {weatherData.CurrentWeather.WindSpeed} KM/H | WMO Weather Code: {weatherData.CurrentWeather.WeatherCode} |
            +--------------------------------------------------------------------------------------------------------------------+";
            Console.WriteLine(output); 
            Console.WriteLine("Please enter any key to exit.");    
            Console.ReadKey();          
        }
        
        /// <summary>
        /// Method to read city coordinate data from filepath.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>List of information of cities</returns>
        private static List<CityInfo> ReadCityCoordinateData(string filePath)
        {
            var citiesData = File.ReadAllText(filePath);
            var jsonData = JsonConvert.DeserializeObject<List<CityInfo>>(citiesData);
            if (jsonData != null)
            {
                return jsonData;
            }
            return new List<CityInfo>();
        }

    }
}

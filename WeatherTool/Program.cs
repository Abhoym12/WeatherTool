using Microsoft.Extensions.DependencyInjection;
using WeatherTool.Helpers;

namespace WeatherTool
{
    class Program
    {
        /// <summary>
        /// Main entry point of Application
        /// </summary>
        static async Task Main()
        {
            //Creating a service collection to manage dependencies
            var services = new ServiceCollection();

            //Registering the WeatherServiceProxy as transient dependency
            services.AddTransient<IWeatherServiceProxy,WeatherServiceProxy>();

            //Registering the WeatherDetails as transient dependency
            services.AddTransient<WeatherDetails>();

            //Building the service provider container
            var container = services.BuildServiceProvider();

            //Resolving the WeatherDetails instance from the container
            var weatherDetails = container.GetService<WeatherDetails>();
            if (weatherDetails!=null)
            {
                await weatherDetails.GetWeatherDetails();
            }
            else
            {
                Console.WriteLine("Error processing request. Please try again later;");
            }
        }     
    }
}
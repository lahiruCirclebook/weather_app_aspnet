using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using weather__app.models;

namespace weather__app.Services
{
    public static class ApiService
    {
        private static readonly HttpClient httpClient = new HttpClient();

        //get weather data
        public static async Task<Root> GetWeather(double lat, double lon)
        {
            try
            {
                // Make the API request
                var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=9003dd73e437ac6e827f14a4a7cd47fa", lat, lon));

                // Check if the response is null or empty
                if (string.IsNullOrEmpty(response))
                {
                    Console.WriteLine("The response is empty.");
                    return null;
                }

                // Deserialize the response
                return JsonConvert.DeserializeObject<Root>(response);
            }
            catch (HttpRequestException httpEx)
            {
                // Handle any HTTP-related exceptions
                Console.WriteLine("An error occurred while making the HTTP request: " + httpEx.Message);
                return null;
            }
            catch (JsonException jsonEx)
            {
                // Handle JSON deserialization errors
                Console.WriteLine("An error occurred while deserializing the response: " + jsonEx.Message);
                return null;
            }
            catch (Exception ex)
            {
                // Catch all other exceptions
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                return null;
            }
        }

        //get weather by city name
        public static async Task<Root> GetWeatherByCity(string city)
        {
            try
            {
                // Make the API request
                var response = await httpClient.GetStringAsync(string.Format("api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=9003dd73e437ac6e827f14a4a7cd47fa", city));

                // Check if the response is null or empty
                if (string.IsNullOrEmpty(response))
                {
                    Console.WriteLine("The response is empty.");
                    return null;
                }

                // Deserialize the response
                return JsonConvert.DeserializeObject<Root>(response);
            }
            catch (HttpRequestException httpEx)
            {
                // Handle any HTTP-related exceptions
                Console.WriteLine("An error occurred while making the HTTP request: " + httpEx.Message);
                return null;
            }
            catch (JsonException jsonEx)
            {
                // Handle JSON deserialization errors
                Console.WriteLine("An error occurred while deserializing the response: " + jsonEx.Message);
                return null;
            }
            catch (Exception ex)
            {
                // Catch all other exceptions
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                return null;
            }
        }
    }
}

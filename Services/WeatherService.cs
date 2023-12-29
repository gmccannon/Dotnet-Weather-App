using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace site1.Services;

public class WeatherService 
{
    public async static Task<site1.Models.WeatherData?> GetWeatherDataAsync(string cityInput)
    {
        string apiKey = "b206890eff5832f08b514e4e1821af44";
        string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityInput}&units=imperial&appid={apiKey}";

        // parse the data from the openweather api
        using (var httpClient = new HttpClient())
        {
            
            // await the responce, then Deserialize the json
            string response = await httpClient.GetStringAsync(apiUrl);
            site1.Models.WeatherData? weatherData = System.Text.Json.JsonSerializer.Deserialize<site1.Models.WeatherData>(response);

            return weatherData;

        }
    }
}
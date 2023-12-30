using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace site1.Services;

public class WeatherService 
{
    public async Task<site1.Models.WeatherData?> GetWeatherDataAsync(string cityInput)
    {
        string apiKey = "82757103e32d2ac370184db6a7e7c1ca";
        string unit = site1.GlobalSettings.Values.units;

        string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityInput}&units={unit}&appid={apiKey}";

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
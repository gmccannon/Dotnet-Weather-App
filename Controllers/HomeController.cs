using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using site1.Models;

namespace site1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> Weather(string cityInput)
    {
        ViewData["cityInput"] = cityInput;

        string apiKey = "b206890eff5832f08b514e4e1821af44";
        string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityInput}&units=imperial&appid={apiKey}";

        using (var httpClient = new HttpClient())
        {
            try
            {
                string response = await httpClient.GetStringAsync(apiUrl);

                site1.Models.WeatherData? weatherData = System.Text.Json.JsonSerializer.Deserialize<site1.Models.WeatherData>(response);

                ViewData["weatherTemp"] = weatherData?.main.temp;
                ViewData["mainWeather"] = weatherData?.weather[0].description;
                ViewData["country"] = weatherData?.sys.country;
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Error fetching weather data: {ex.Message}";
            }
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

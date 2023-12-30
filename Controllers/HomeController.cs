using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace site1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly site1.Services.WeatherService _weatherService;

    public HomeController(ILogger<HomeController> logger, site1.Services.WeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
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
        try
        {
            // Await the task to get the actual result
            site1.Models.WeatherData? weatherData = await _weatherService.GetWeatherDataAsync(cityInput);

            // Check if weatherData is not null before accessing properties
            if (weatherData != null)
            {
                ViewData["city"] = weatherData.name;
                ViewData["weatherTemp"] = weatherData.main.temp;
                ViewData["mainWeather"] = weatherData.weather[0].description;
                ViewData["country"] = weatherData.sys.country;
            }

            return View();
        }
        catch (Exception)
        {
            return RedirectToAction("Error");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new site1.Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
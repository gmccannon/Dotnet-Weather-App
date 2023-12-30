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

    public IActionResult Settings(string units, string color)
    {      
        /*This controller handles both going to the setting page as well as changing the settings, if there is no input,
        handle this as if the request was to go to the setting tab. */
        if (units != "imperial" && units != "metric") return View();

        /*If there is an input*/
        site1.GlobalSettings.Values.units = units;
        site1.GlobalSettings.Values.colorMode = color;
        return View();
    }

    public async Task<IActionResult> Weather(string cityInput)
    {   
        try
        {
            // Await the task to get the actual result
            ViewData["WeatherData"] = await _weatherService.GetWeatherDataAsync(cityInput);
            
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
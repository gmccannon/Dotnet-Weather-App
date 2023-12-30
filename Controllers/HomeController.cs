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

    public IActionResult About()
    {
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
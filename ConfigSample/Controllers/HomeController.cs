using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ConfigSample.Models;
using Microsoft.Extensions.Options;

namespace ConfigSample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppSettings _setting;

    public HomeController(
        ILogger<HomeController> logger,
        IOptions<AppSettings> options)
    {
        _logger = logger;
        _setting = options.Value;
    }

    public IActionResult Index()
    {
        ViewData["Message"] = _setting.Bar;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

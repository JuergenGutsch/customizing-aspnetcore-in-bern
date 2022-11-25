using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CachingSample.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CachingSample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMemoryCache _cache;

    public HomeController(ILogger<HomeController> logger,
        IMemoryCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    [ResponseCache(CacheProfileName = "MyProfile")]
    public IActionResult Index()
    {
        if (!_cache.TryGetValue<DateTime>("Time", out var mytime))
        {
            mytime = DateTime.Now;
            _cache.Set("Time", mytime, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(5)
            });
        }

        var your = _cache.GetOrCreate<DateTime>("NewTime", entry =>
        {
            entry.AbsoluteExpiration = DateTime.Now.AddSeconds(5);
            return DateTime.Now;
        });

        return View(mytime);
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

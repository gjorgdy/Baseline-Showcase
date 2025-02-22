using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Baseline.Models;
using Baseline.Models.Tiles;

namespace Baseline.Controllers;

public class ProfileController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        return View(HardcodedData.ProfileJordy);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
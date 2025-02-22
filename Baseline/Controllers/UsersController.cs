using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Baseline.Models;

namespace Baseline.Controllers;

public class UsersController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        return RedirectToAction("Search");
    }

    public IActionResult Search()
    {
        return View();
    }

    public IActionResult Profile(int? id)
    {
        return id == 0
            ? View(HardcodedData.ProfileJordy)
            : RedirectToAction("search");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
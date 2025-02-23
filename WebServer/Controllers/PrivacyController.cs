using System.Diagnostics;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers;

public class PrivacyController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
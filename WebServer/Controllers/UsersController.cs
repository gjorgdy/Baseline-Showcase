using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Core.Models;

namespace Core.Controllers;

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

    [HttpGet]
    public IActionResult Mail(int? id)
    {
        return id == 0
            ? View(new MailFormModel(id.GetValueOrDefault()))
            : RedirectToAction("search");
    }

    [HttpPost]
    public IActionResult Mail([FromForm] MailFormModel model)
    {
        Baseline.MailService.SendMail(
            model.Email,
            "jordyreins@gmail.com",
            model.FirstName,
            model.Message
        );
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
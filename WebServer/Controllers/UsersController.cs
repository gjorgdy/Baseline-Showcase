using System.Diagnostics;
using Core;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers;

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

    [Route("users/{id:int}/profile")]
    public IActionResult Profile(int id)
    {
        return id == 0
            ? View(HardcodedData.ProfileJordy)
            : RedirectToAction("search");
    }

    [HttpGet]
    [Route("users/{id:int}/mail")]
    public IActionResult Mail(int id)
    {
        return id == 0
            ? View(new MailFormModel(id))
            : RedirectToAction("search");
    }

    [HttpPost]
    [Route("users/{id:int}/mail")]
    public IActionResult Mail([FromForm] MailFormModel model, int id)
    {
        if (model.Validate())
        {
            Baseline.MailService.SendMail(
                model.Email,
                "jordyreins@gmail.com",
                model.FirstName,
                model.Message
            );
        }
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
using System.Diagnostics;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers;

public class UsersController : Controller
{

    public IActionResult Search()
    {
        return View();
    }

    [Route("users/{id:int}")]
    public IActionResult Index(int id)
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
using Core.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers;

public class AuthController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View(AuthPageModel.GetInstance());
    }

    // [Authorize]
    public async Task<IActionResult> Success(string code)
    {
        var auth = new DiscordAuth();
        _ = await auth.RequestTokens(code);

        return View(new AuthStatusModel("Success: " + await auth.GetUserData()));
    }
}
using Core.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View(AuthPageModel.GetInstance());
    }
    
    [Route("success")]
    public IActionResult Success(string response)
    {
        return View(new AuthStatusModel(response));
    }

    [Route("callback/discord")]
    public async Task<IActionResult> Discord(string code)
    {
        var auth = new DiscordAuth();
        _ = await auth.RequestTokens(code);

        return RedirectToAction("Success", "Auth");
        // return View(new AuthStatusModel("Success: " + await auth.GetUserData()));
    }

    [Route("callback/google")]
    public async Task<IActionResult> Google(string code)
    {
        await Console.Out.WriteLineAsync("Google Authenticator");
        var auth = new GoogleAuth();
        _ = await auth.RequestTokens(code);
        
        // return RedirectToAction("Success", "Auth");
        return View(new AuthStatusModel(auth.GetUserSub() ?? "No Sub"));
    }
    
}
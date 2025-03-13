using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core;
using Core.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    public IActionResult Index() => View();
    
    [Route("success")]
    public IActionResult Success(string response)
    {
        TempData.TryGetValue("Model", out object? value);
        return View(new AuthStatusModel(
            value?.ToString() ?? "No Auth request was received"
        ));
    }

    [Route("callback/{platform}")]
    public async Task<IActionResult> Callback(string code, string platform)
    {
        var handler = Baseline.GetOAuthPlatform(platform).GetOAuthHandler();
        _ = await handler.RequestTokens(code);

        TempData["Model"] = await handler.GetUserId() ?? "No Sub";
        return RedirectToAction("Success", "Auth");
    }
    
}
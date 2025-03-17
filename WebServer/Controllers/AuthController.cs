using Core;
using Core.Authentication;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace WebServer.Controllers;

[Route("auth")]
public class AuthController(UserService userService) : Controller
{
    public IActionResult Index()
    {
        string? token = HttpContext.Request.Cookies["JwtToken"];
        if (token == null)
            return View();
        var tokenHandler = new JwtTokenHandler();
        TempData["Model"] = tokenHandler.ValidateToken(token);
        return RedirectToAction("LoggedIn", "Auth");
    }

    [Route("logged-in")]
    public IActionResult LoggedIn()
    {
        TempData.TryGetValue("Model", out object? value);
        return View(new AuthStatusModel(
            value?.ToString() ?? "No Auth request was received"
        ));
    }
    
    [Route("success")]
    public async Task<IActionResult> Success(string response)
    {
        TempData.TryGetValue("Model", out object? value);
        // Adding a cookie
        await Console.Out.WriteLineAsync($"UserValue: {value}");
        var tokenHandler = new JwtTokenHandler();
        HttpContext.Response.Cookies.Append(
            "JwtToken", 
            tokenHandler.CreateToken(value?.ToString()!), 
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(30),
                HttpOnly = true, // Accessible only by the server
                IsEssential = true, // Required for GDPR compliance
                Secure = true,
                SameSite = SameSiteMode.Strict
            }
        );
        
        return View(new AuthStatusModel(
            value?.ToString() ?? "No Auth request was received"
        ));
    }

    [Route("callback/{platform}")]
    public async Task<IActionResult> Callback(string code, string platform)
    {
        var handler = Baseline.GetOAuthPlatform(platform).GetOAuthHandler();
        _ = await handler.RequestTokens(code);

        string? platformId = await handler.GetUserId();
        if (platformId != null)
        {
            var user = await userService.GetUser(platform, platformId);
            await Console.Out.WriteLineAsync($"User: {user?.Id}");
            TempData["Model"] = user?.Id;
        }
        else
        {
            TempData["Model"] = null;
        }
        return RedirectToAction("Success", "Auth");
    }
    
}
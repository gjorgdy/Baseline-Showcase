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
        return View();
        // string? token = HttpContext.Request.Cookies["JwtToken"];
        // if (token == null) return View();
        // var tokenHandler = new JwtTokenHandler();
        // string userId = tokenHandler.ValidateToken(token);
        // TempData["UserId"] = userId;
        // return RedirectToAction("LoggedIn", "Auth");
    }

    [Route("logged-in")]
    public IActionResult LoggedIn()
    {
        TempData.TryGetValue("UserId", out object? value);
        return View(new AuthStatusModel(
            value == null 
                ? "Token is no longer valid."
                : $"Logged in as user {value}"
        ));
    }
    
    [Route("success")]
    public IActionResult Success(string response)
    {
        TempData.TryGetValue("UserId", out object? value);
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
            TempData["UserId"] = user?.Id;
        }
        else
        {
            TempData["UserId"] = null;
        }
        return RedirectToAction("Success", "Auth");
    }
    
}
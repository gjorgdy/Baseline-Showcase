using Core;
using Core.Authentication;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace WebServer.Controllers;

[Route("auth")]
public class AuthController(UserService userService) : AbstractController
{
    public async Task<IActionResult> Index()
    {
        int? userId = await ReadUserId();
        if (userId == null) return View();
        TempData["UserId"] = userId;
        return RedirectToAction("Index", "Users", new { id = userId });
    }
    
    [Route("success")]
    public IActionResult Success(string response)
    {
        TempData.TryGetValue("UserId", out object? value);
        var tokenHandler = new JwtTokenHandler();
        // Store token in cookie
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
        //
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
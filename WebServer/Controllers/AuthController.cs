using Core;
using Core.Authentication;
using Core.Models;
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
    }
    
    [Route("logout")]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("JwtToken", new CookieOptions
        {
            SameSite = SameSiteMode.None,
            Secure = true
        });
        return RedirectToAction("index", "auth");
    }
    
    [Route("success")]
    public IActionResult Success(string response)
    {
        TempData.TryGetValue("UserId", out object? value);
        // Store token in cookie
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
        // Get callback code
        string? platformId = await handler.GetUserId();
        if (platformId == null) return RedirectToAction("Index", "Auth");
        // Get connected user
        var user = 
            await userService.GetUserData(platform, platformId) 
            ?? await userService.NewUser(platform, platformId);
        if (user == null) return RedirectToAction("Index", "Auth");
        // Return token to client and send to profile
        AppendCookie(user);
        return RedirectToAction("Index", "Users", new { id = user.Id });
    }
    
    private void AppendCookie(UserData user) 
    {
        var tokenHandler = new JwtTokenHandler();
        HttpContext.Response.Cookies.Append(
            "JwtToken", 
            tokenHandler.CreateToken(user), 
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(30),
                HttpOnly = true, // Accessible only by the server
                IsEssential = true, // Required for GDPR compliance
                Secure = true,
                SameSite = SameSiteMode.Strict
            }
        );
    }
    
}
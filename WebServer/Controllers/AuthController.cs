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
        int? userId = JwtTokenHandler.GetUserId(User);
        if (userId == null) return View();
        TempData["UserId"] = userId;
        return RedirectToAction("Index", "Users", new { id = userId });
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
        var user = await userService.GetUser(platform, platformId);
        if (user == null) return NotFound();
        // Return token to client and send to profile
        AppendCookie(user.Id);
        return RedirectToAction("Index", "Users", new { id = user.Id });
    }
    
    private void AppendCookie(int userId) 
    {
        var tokenHandler = new JwtTokenHandler();
        HttpContext.Response.Cookies.Append(
            "JwtToken", 
            tokenHandler.CreateToken(userId), 
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
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers;

public abstract class AbstractController : Controller
{

    protected async Task<int?> ReadUserId()
    {
        int userId;
        try
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            userId = int.Parse(claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
        catch (Exception e)
        {
            await Console.Out.WriteLineAsync(e.Message);
            return null;
        }
        return userId;
    }
    
}
using Core.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers;

public abstract class AAuthController : Controller
{
    protected int? ValidateToken()
    {
        string? token = HttpContext.Request.Cookies["JwtToken"];
        if (token == null) return null;
        var tokenHandler = new JwtTokenHandler();
        string idString = tokenHandler.ValidateToken(token);
        return int.Parse(idString);
    }
}
using Core.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers;

[Route("users/me")]
public class AuthController : Controller
{

    [HttpGet]
    public IActionResult GetLoggedInUserId()
    {
        int? loggedInUserId = JwtTokenHandler.GetUserId(User);
        if (loggedInUserId == null) return NotFound();
        return Ok(new { UserId = loggedInUserId });
    }
    
}
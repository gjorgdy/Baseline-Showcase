using System.Security.Claims;
using System.Text.Encodings.Web;
using Core.Authentication;
using Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace ApiServer;

public class MyAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder, UserService userService)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Cookies.ContainsKey("JwtToken"))
        {
            return AuthenticateResult.Fail("No JWT token");
        }
        
        string token = Request.Cookies["JwtToken"]!;
        
        var tokenHandler = new JwtTokenHandler();
        string idString = tokenHandler.ValidateToken(token);

        if (string.IsNullOrEmpty(idString))
        {
            return AuthenticateResult.Fail("Invalid JWT token");
        }

        var user = await userService.GetUser(int.Parse(idString));
        if (user == null)
        {
            return AuthenticateResult.Fail("Invalid JWT token");
        }
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, idString),
            new Claim(ClaimTypes.Name, user.DisplayName),
            new Claim(ClaimTypes.Uri, user.ProfilePicture),
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
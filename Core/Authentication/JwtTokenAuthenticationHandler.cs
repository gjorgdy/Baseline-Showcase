using System.Security.Claims;
using System.Text.Encodings.Web;
using Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Core.Authentication;

public class JwtTokenAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISystemClock clock)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder, clock)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Cookies.ContainsKey("JwtToken"))
        {
            return Task.FromResult(AuthenticateResult.Fail("No JWT token"));
        }
        
        string token = Request.Cookies["JwtToken"]!;
        
        ClaimsPrincipal principal;
        try
        {
            principal = new JwtTokenHandler().ValidateToken(token);
        }
        catch (SecurityTokenException e)
        {
            return Task.FromResult(AuthenticateResult.Fail(e.Message));
        }
        
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
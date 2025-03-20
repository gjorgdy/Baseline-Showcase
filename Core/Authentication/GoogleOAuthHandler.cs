using System.IdentityModel.Tokens.Jwt;
using Core.Models;

namespace Core.Authentication;

public class GoogleOAuthHandler(OAuthPlatformModel platformModel)
    : AbstractOAuthHandler(platformModel, "https://oauth2.googleapis.com/token")
{

    public override Task<string?> GetUserId()
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(Tokens["id_token"]) as JwtSecurityToken;
        return Task.FromResult(jsonToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value);
    }
    
}
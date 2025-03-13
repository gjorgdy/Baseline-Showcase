using System.IdentityModel.Tokens.Jwt;

namespace Core.Authentication;

public class GoogleOAuthHandler(OAuthPlatform platform)
    : AbstractOAuthHandler(platform, "https://oauth2.googleapis.com/token")
{

    public override Task<string?> GetUserId()
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(Tokens["id_token"]) as JwtSecurityToken;
        return Task.FromResult(jsonToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value);
    }
    
}
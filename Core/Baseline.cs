using Core.Authentication;
using Core.Models;
using Core.Platforms;

namespace Core;

public class Baseline
{
    private static readonly Dictionary<string, OAuthPlatformModel> OAuthPlatforms = [];

    public Baseline()
    {
        OAuthPlatforms.Add(
            "google", OAuthPlatformModel.Load("GOOGLE", p => new GoogleOAuthHandler(p))
        );
        OAuthPlatforms.Add(
            "discord", OAuthPlatformModel.Load("DISCORD", p => new DiscordOAuthHandler(p))
        );
    }
    
    public static OAuthPlatformModel GetOAuthPlatform(string platform) => OAuthPlatforms[platform];
    public static IEnumerable<OAuthPlatformModel> GetAllOAuthPlatforms() => OAuthPlatforms.Values;
}
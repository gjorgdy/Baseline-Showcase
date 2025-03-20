using System.Runtime.Serialization;
using Core.Authentication;
using Core.Interfaces;
using Core.Models;

namespace Core;

public class Baseline
{
    private static readonly Dictionary<string, OAuthPlatformModel> OAuthPlatforms = [];
    public static HttpClient HttpClient { get; private set; } = null!;
    // public static IDataAccess DataAccess { get; private set; } = null!;

    public Baseline()
    {
        HttpClient = new HttpClient();
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
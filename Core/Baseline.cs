using Core.Authentication;
using Core.Models;
using Core.Platforms;

namespace Core;

public class Baseline
{
    private static readonly Dictionary<string, OAuthPlatformModel> OAuthPlatforms = [];
    public static HttpClient HttpClient { get; private set; } = null!;
    public static DiscordApiHandler DiscordApi { get; private set; } = null!;

    public Baseline()
    {
        HttpClient = new HttpClient();
        DiscordApi = new DiscordApiHandler(HttpClient);
        Console.Out.WriteLine("[LOG] Loaded Discord API");
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
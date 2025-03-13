using Core.Authentication;
using Core.Interfaces;

namespace Core;

public class Baseline
{
    private static readonly Dictionary<string, OAuthPlatform> OAuthPlatforms = [];

    public static IMail MailService { get; private set; } = null!;
    
    public static HttpClient HttpClient { get; private set; } = null!;

    public Baseline(IMail mailService)
    {
        MailService = mailService;
        HttpClient = new HttpClient();
        OAuthPlatforms.Add(
            "google", OAuthPlatform.Load("GOOGLE", p => new GoogleOAuthHandler(p))
        );
        OAuthPlatforms.Add(
            "discord", OAuthPlatform.Load("DISCORD", p => new DiscordOAuthHandler(p))
        );
    }
    
    public static OAuthPlatform GetOAuthPlatform(string platform) => OAuthPlatforms[platform];
    public static IEnumerable<OAuthPlatform> GetAllOAuthPlatforms() => OAuthPlatforms.Values;

}
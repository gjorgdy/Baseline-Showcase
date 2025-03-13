using dotenv.net;

namespace Core.Authentication;

public class OAuthPlatform
{
    
    public string PlatformName { get; init; }
    public string RedirectUri { get; init; }
    public string OAuthUrl { get; init; }
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }

    private readonly Func<OAuthPlatform, AbstractOAuthHandler> _handlerFactory; 
    
    private OAuthPlatform(string platformName, string redirectUri, string oAuthUrl, string clientId, string clientSecret, Func<OAuthPlatform, AbstractOAuthHandler> handlerFactory)
    {
        PlatformName = platformName;
        RedirectUri = redirectUri;
        OAuthUrl = oAuthUrl;
        ClientId = clientId;
        ClientSecret = clientSecret;
        _handlerFactory = handlerFactory;
    }

    public static OAuthPlatform Load(string platformId, Func<OAuthPlatform, AbstractOAuthHandler> handlerFactory)
    {
        DotEnv.Load();
        var env = DotEnv.Read();
        return new OAuthPlatform(
            env[platformId + "_PLATFORM_NAME"],
            env["OAUTH_REDIRECT_ROOT"] + env[platformId + "_REDIRECT_PATH"],
            env[platformId + "_OAUTH_URL"],
            env[platformId + "_CLIENT_ID"],
            env[platformId + "_CLIENT_SECRET"],
            handlerFactory
        );
    }

    public AbstractOAuthHandler GetOAuthHandler() => _handlerFactory(this);
    
}
using System.Collections;
using Core.Authentication;

namespace Core.Models;

public class OAuthPlatformModel
{
    
    public string PlatformName { get; init; }
    public string RedirectUri { get; init; }
    public string OAuthUrl { get; init; }
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }

    private readonly Func<OAuthPlatformModel, AbstractOAuthHandler> _handlerFactory; 
    
    private OAuthPlatformModel(string platformName, string redirectUri, string oAuthUrl, string clientId, string clientSecret, Func<OAuthPlatformModel, AbstractOAuthHandler> handlerFactory)
    {
        PlatformName = platformName;
        RedirectUri = redirectUri;
        OAuthUrl = oAuthUrl;
        ClientId = clientId;
        ClientSecret = clientSecret;
        _handlerFactory = handlerFactory;
    }

    public static OAuthPlatformModel Load(string platformId, Func<OAuthPlatformModel, AbstractOAuthHandler> handlerFactory)
    {
        var env = (Hashtable) Environment.GetEnvironmentVariables();
        return new OAuthPlatformModel(
            (string) env[platformId + "_PLATFORM_NAME"]!,
            (string) env["OAUTH_REDIRECT_ROOT"]! + env[platformId + "_REDIRECT_PATH"],
            (string) env[platformId + "_OAUTH_URL"]!,
            (string) env[platformId + "_CLIENT_ID"]!,
            (string) env[platformId + "_CLIENT_SECRET"]!,
            handlerFactory
        );
    }

    public AbstractOAuthHandler GetOAuthHandler() => _handlerFactory(this);
    
}
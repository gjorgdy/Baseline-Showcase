using Core.Models;
using Newtonsoft.Json;

namespace Core.Authentication;

public abstract class AbstractOAuthHandler(OAuthPlatformModel platformModel, string tokenEndpoint)
{

    protected Dictionary<string, string> Tokens = new();

    public async Task<bool> RequestTokens(string authorizationCode)
    {
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", platformModel.ClientId },
            { "client_secret", platformModel.ClientSecret },
            { "grant_type", "authorization_code" },
            { "code", authorizationCode },
            { "redirect_uri", platformModel.RedirectUri }
        });

        var response = await Baseline.HttpClient.PostAsync(tokenEndpoint, content);

        if (!response.IsSuccessStatusCode) throw new HttpRequestException(response.ReasonPhrase);

        string responseString = await response.Content.ReadAsStringAsync();
        Tokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString) ?? [];
        return true;
    }

    public abstract Task<string?> GetUserId();

}
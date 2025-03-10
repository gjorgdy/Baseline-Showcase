using dotenv.net;
using Newtonsoft.Json;

namespace Core.Authentication;

public class DiscordAuth
{

    private static readonly HttpClient Client = new HttpClient();

    private Dictionary<string, string> _discordTokens = new();

    public async Task<bool> RequestTokens(string authorizationCode)
    {
        const string tokenEndpoint = "https://discord.com/api/oauth2/token";

        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", DotEnv.Read()["DISCORD_CLIENT_ID"] },
            { "client_secret", DotEnv.Read()["DISCORD_CLIENT_SECRET"] },
            { "grant_type", "authorization_code" },
            { "code", authorizationCode },
            { "redirect_uri", "https://localhost:7166/auth/success" },
            { "scope", "identify, offline" }  // Ensure this matches the scopes you requested
        });

        var response = await Client.PostAsync(tokenEndpoint, content);

        if (!response.IsSuccessStatusCode) return false;

        string responseString = await response.Content.ReadAsStringAsync();
        _discordTokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString) ?? [];
        return true;
    }

    public async Task<string?> GetUserData()
    {
        const string userEndpoint = "https://discord.com/api/v10/users/@me";

        var request = new HttpRequestMessage(HttpMethod.Get, userEndpoint)
        {
            Headers =
            {
                { "Authorization", $"Bearer {GetAccessToken()}" }
            }
        };

        var response = await Client.SendAsync(request);

        if (!response.IsSuccessStatusCode) return null; // If there is an error, return null
        string userData = await response.Content.ReadAsStringAsync();
        return userData; // Return the user data

    }

    public string GetAccessToken()
    {
        return _discordTokens["access_token"];
    }

    public string GetRefreshToken()
    {
        return _discordTokens["refresh_token"];
    }

}
using System.IdentityModel.Tokens.Jwt;
using dotenv.net;
using Newtonsoft.Json;

namespace Core.Authentication;

public class GoogleAuth
{

    private static readonly HttpClient Client = new HttpClient();

    private Dictionary<string, string> _tokens = new();

    public async Task<bool> RequestTokens(string authorizationCode)
    {
        const string tokenEndpoint = "https://oauth2.googleapis.com/token";

        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", DotEnv.Read()["GOOGLE_CLIENT_ID"] },
            { "client_secret", DotEnv.Read()["GOOGLE_CLIENT_SECRET"] },
            { "grant_type", "authorization_code" },
            { "code", authorizationCode },
            { "redirect_uri", "https://localhost:44350/auth/callback/google" }
        });

        var response = await Client.PostAsync(tokenEndpoint, content);

        if (!response.IsSuccessStatusCode) return false;

        string responseString = await response.Content.ReadAsStringAsync();
        _tokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString) ?? [];
        
        return true;
    }

    public string? GetUserSub()
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(_tokens["id_token"]) as JwtSecurityToken;
        return jsonToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
    }
    
}
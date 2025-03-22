using System.Text.Json;
using System.Text.Json.Serialization;
using dotenv.net;

namespace Core.Platforms;

public class DiscordApiHandler
{

    private string BotToken { get; init; }
    private HttpClient HttpClient { get; init; }
    
    public DiscordApiHandler(HttpClient httpClient)
    {
        HttpClient = httpClient;
        
        DotEnv.Load();
        var env = DotEnv.Read(); 
        BotToken = env["DISCORD_BOT_TOKEN"];
    }
    
    public async Task<string?> GetDisplayName(string userId)
    {
        return (await GetUser(userId))?.DisplayName;
    }
    
    public async Task<string?> GetProfilePictureUri(string userId)
    {
        string? hash = (await GetUser(userId))?.AvatarHash;
        return $"https://cdn.discordapp.com/avatars/{userId}/{hash}.png";
    }

    private async Task<DiscordUserModel?> GetUser(string userId)
    {
        string userEndpoint = $"https://discord.com/api/v10/users/{userId}";
        var request = new HttpRequestMessage(HttpMethod.Get, userEndpoint)
        {
            Headers =
            {
                { "Authorization", $"Bot {BotToken}" }
            }
        };
        var response = await Baseline.HttpClient.SendAsync(request);
        string responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<DiscordUserModel>(responseContent);
    }

    public class DiscordUserModel
    {
        [JsonPropertyName("global_name")]
        public string? DisplayName { get; init; }
        [JsonPropertyName("avatar")]
        public string? AvatarHash { get; init; }
    }
    
}
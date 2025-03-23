using System.Text.Json;
using System.Text.Json.Serialization;
using dotenv.net;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Platforms;

public class DiscordApiHandler
{

    private string BotToken { get; init; }
    private HttpClient HttpClient { get; init; }
    private IMemoryCache MemoryCache { get; init; }
    
    public DiscordApiHandler(HttpClient httpClient, IMemoryCache memoryCache)
    {
        HttpClient = httpClient;
        MemoryCache = memoryCache;
        
        DotEnv.Load();
        var env = DotEnv.Read(); 
        BotToken = env["DISCORD_BOT_TOKEN"];
    }
    
    public async Task<string?> GetDisplayName(string userId)
    {
        MemoryCache.TryGetValue(userId + ":dp", out string? displayName);
        if (displayName != null) return displayName;
        displayName = (await GetUser(userId))?.DisplayName;
        MemoryCache.Set(userId + ":dp", displayName, TimeSpan.FromMinutes(5));
        return displayName;
    }
    
    public async Task<string?> GetProfilePictureUri(string userId)
    {
        MemoryCache.TryGetValue(userId + ":pfp", out string? profilePictureUri);
        if (profilePictureUri != null) return profilePictureUri;
        string? hash = (await GetUser(userId))?.AvatarHash;
        profilePictureUri = $"https://cdn.discordapp.com/avatars/{userId}/{hash}.png";
        MemoryCache.Set(userId + ":pfp", profilePictureUri, TimeSpan.FromMinutes(5));
        return profilePictureUri;
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
        var response = await HttpClient.SendAsync(request);
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
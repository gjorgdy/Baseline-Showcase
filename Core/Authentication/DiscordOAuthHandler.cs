﻿using Core.Models;
using Newtonsoft.Json;

namespace Core.Authentication;

public class DiscordOAuthHandler(OAuthPlatformModel platformModel)
    : AbstractOAuthHandler(platformModel, "https://discord.com/api/oauth2/token")
{

    private readonly HttpClient _httpClient = new HttpClient();
    
    public override async Task<string?> GetUserId()
    {
        var userData = JsonConvert.DeserializeObject<Dictionary<string, string>>(await GetUserData() ?? "") ?? [];
        return userData.GetValueOrDefault("id");
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

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode) return null; // If there is an error, return null
        string userData = await response.Content.ReadAsStringAsync();
        return userData; // Return the user data

    }

    public string GetAccessToken()
    {
        return Tokens["access_token"];
    }

    public string GetRefreshToken()
    {
        return Tokens["refresh_token"];
    }

}
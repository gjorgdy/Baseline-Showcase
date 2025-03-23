using Core.Interfaces;
using Core.Models;
using Core.Platforms;

namespace Core.Services;

public class UserService(DiscordApiHandler discordApi, IUserAccess userAccess)
{
    public async Task<UserModel?> GetUser(int id)
    {
        var user = await userAccess.GetUser(id);
        return user != null ? await ToUserModel(user) : null;
    }

    public async Task<UserModel?> GetUser(string platform, string platformId) 
    {
        var user = await userAccess.GetUser(platform, platformId);
        return user != null ? await ToUserModel(user) : null;
    }

    public async Task<UserModel?> NewUser(string platform, string platformId)
    {
        var userData = await userAccess.NewUser(platform, platformId);
        return await ToUserModel(userData);
    }
    
    public Task<bool> SetDisplayName(int id, string displayName) => userAccess.SetDisplayName(id, displayName);

    public Task<bool> SetProfilePicture(int id, string picture) => userAccess.SetProfilePicture(id, picture);

    private async Task<UserModel?> ToUserModel(UserData? userData)
    {
        string? displayName = null;
        if (userData?.DisplayNamePlatform == "discord")
        {
            string? discordId = userData.Connections
                .Where(connection => connection.Platform == "discord")
                .Select(connection => connection.PlatformId)
                .FirstOrDefault();
            if (discordId != null)
            {
                displayName = await discordApi.GetDisplayName(discordId);
            }
        }
        string? profilePictureUri = null;
        if (userData?.ProfilePicturePlatform == "discord")
        {
            string? discordId = userData.Connections
                .Where(connection => connection.Platform == "discord")
                .Select(connection => connection.PlatformId)
                .FirstOrDefault();
            if (discordId != null)
            {
                profilePictureUri = await discordApi.GetProfilePictureUri(discordId);
            }
        }
        
        if (userData != null)
            return new UserModel(
                userData.Id,
                displayName ?? "{Not Found}",
                profilePictureUri ?? "/resources/default_pfp.jpg"
            );
        else return null;
    }
    
}
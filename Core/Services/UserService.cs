using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class UserService(IUserAccess userAccess) : IUserAccess
{
    public async Task<UserModel?> GetUser(int id)
    {
        var user = await userAccess.GetUser(id);
        if (user == null) return null;
        // Retrieve name and pfp from API
        return new UserModel(
            user.Id,
            "DisplayName",
            "/resources/pfp.jpg"
        );
    }

    public Task<UserModel?> GetUser(string platform, string platformId) => userAccess.GetUser(platform, platformId);

    public Task<bool> SetDisplayName(int id, string displayName) => userAccess.SetDisplayName(id, displayName);

    public Task<bool> SetProfilePicture(int id, string picture) => userAccess.SetProfilePicture(id, picture);
}
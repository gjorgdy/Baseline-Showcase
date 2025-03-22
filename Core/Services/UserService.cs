using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class UserService(IUserAccess userAccess)
{
    public async Task<UserModel?> GetUser(int id)
    {
        var user = await userAccess.GetUser(id);
        return user != null ? ToUserModel(user) : null;
    }

    public async Task<UserModel?> GetUser(string platform, string platformId) 
    {
        var user = await userAccess.GetUser(platform, platformId);
        return user != null ? ToUserModel(user) : null;
    }

    public Task<bool> SetDisplayName(int id, string displayName) => userAccess.SetDisplayName(id, displayName);

    public Task<bool> SetProfilePicture(int id, string picture) => userAccess.SetProfilePicture(id, picture);

    private static UserModel ToUserModel(UserData userData)
    {
        return new UserModel(
            userData.Id,
            "DisplayName",
            "/resources/pfp.jpg"
        );
    }
    
}
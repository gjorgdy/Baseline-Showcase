using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class UserService(IUserAccess userAccess) : IUserAccess
{
    public Task<UserModel?> GetUser(int id) => userAccess.GetUser(id);

    public Task<UserModel?> GetUser(string platform, string platformId) => userAccess.GetUser(platform, platformId);

    public Task<bool> SetDisplayName(int id, string displayName) => userAccess.SetDisplayName(id, displayName);

    public Task<bool> SetProfilePicture(int id, string picture) => userAccess.SetProfilePicture(id, picture);
}
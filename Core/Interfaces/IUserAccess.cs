using Core.Models;

namespace Core.Interfaces;

public interface IUserAccess
{
    public Task<UserModel?> GetUser(int id);
    public Task<UserModel?> GetUser(string platform, string platformId);
    public Task<bool> SetDisplayName(int id, string displayName);
    public Task<bool> SetProfilePicture(int id, string picture);
}
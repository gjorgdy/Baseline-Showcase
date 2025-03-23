using Core.Models;

namespace Core.Interfaces;

public interface IUserAccess
{
    public Task<UserData?> NewUser(string platform, string platformId);
    public Task<UserData?> GetUser(int id);
    public Task<UserData?> GetUser(string platform, string platformId);
    public Task<bool> SetDisplayName(int id, string displayName);
    public Task<bool> SetProfilePicture(int id, string picture);
}
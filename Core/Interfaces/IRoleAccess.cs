namespace Core.Interfaces;

public interface IRoleAccess
{
    public Task<bool> CreateRole(string platform, string id, string displayName);
    public Task<bool> UpdateRole(string platform, string id, string displayName);
    public Task<bool> DeleteRole(string platform, string id);
}
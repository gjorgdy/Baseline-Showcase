using Core.Interfaces;

namespace Core.Services;

public class RoleService(IRoleAccess roleAccess) : IRoleAccess
{
    public Task<bool> CreateRole(string platform, string id, string displayName)
        => roleAccess.CreateRole(platform, id, displayName);

    public Task<bool> UpdateRole(string platform, string id, string displayName)
        => roleAccess.UpdateRole(platform, id, displayName);

    public Task<bool> DeleteRole(string platform, string id)
        => roleAccess.DeleteRole(platform, id);
}
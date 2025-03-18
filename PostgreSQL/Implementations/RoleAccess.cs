using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using PostgreSQL.Models;

namespace PostgreSQL.Implementations;

public class RoleAccess(PostgresDbContext dbContext) : IRoleAccess
{
    private async Task<RoleEntity?> GetRole(string platform, string id)
    {
        return await dbContext.Roles.FirstOrDefaultAsync(r => 
            r.Platform == platform && r.Id == id
        );
    }
    
    public async Task<bool> CreateRole(string platform, string id, string displayName)
    {
        dbContext.Roles.Add(new RoleEntity
        {
            Platform = platform,
            Id = id,
            DisplayName = displayName,
        });
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateRole(string platform, string id, string displayName)
    {
        var role = await GetRole(platform, id);
        if (role == null) return false;
        role.DisplayName = displayName;
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteRole(string platform, string id)
    {
        var role = await GetRole(platform, id);
        if (role == null) return false;
        dbContext.Roles.Remove(role);
        return await dbContext.SaveChangesAsync() > 0;
    }
    
}
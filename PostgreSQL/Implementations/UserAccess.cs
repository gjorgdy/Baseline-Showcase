using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using PostgreSQL.Models;

namespace PostgreSQL.Implementations;

public class UserAccess(PostgresDbContext dbContext) : IUserAccess
{
    private async Task<UserEntity?> GetUserEntity(int id)
    {
        return await dbContext.Users
            .Include(u => u.Connections)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    private async Task<UserEntity?> GetUserEntity(string platform, string platformId)
    {
        var conn = await dbContext.Connections
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Platform == platform && c.PlatformId == platformId);
        return conn?.User;
    }
    
    public async Task<UserData?> GetUser(int id)
    {
        var userEntity = await GetUserEntity(id);
        return userEntity?.GetDataModel();
    }
    
    public async Task<UserData?> GetUser(string platform, string platformId)
    {
        var userEntity = await GetUserEntity(platform, platformId);
        return userEntity?.GetDataModel();
    }

    public async Task<bool> SetDisplayName(int id, string displayName)
    {
        var user = await GetUserEntity(id);
        if (user == null) return false;
        user.DisplayName = displayName;
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> SetProfilePicture(int id, string picture)
    {
        var user = await GetUserEntity(id);
        if (user == null) return false;
        user.ProfilePicture = picture;
        return await dbContext.SaveChangesAsync() > 0;
    }
}
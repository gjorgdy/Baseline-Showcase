﻿using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using PostgreSQL.Models;

namespace PostgreSQL.Access;

public class UserAccess(PostgresDbContext dbContext) : IUserAccess
{
    private async Task<UserEntity?> GetUserEntity(int id) => await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    private async Task<UserEntity?> GetUserEntity(string platform, string platformId)
    {
        var conn = await dbContext.Connections
            .Include(c => c.UserEntity)
            .FirstOrDefaultAsync(c => c.Platform == platform && c.PlatformId == platformId);
        return conn?.UserEntity;
    }
    
    public async Task<UserModel?> GetUser(int id)
    {
        var userEntity = await GetUserEntity(id);
        return userEntity?.GetModel();
    }
    
    public async Task<UserModel?> GetUser(string platform, string platformId)
    {
        var userEntity = await GetUserEntity(platform, platformId);
        return userEntity?.GetModel();
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
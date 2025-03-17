using Core.Interfaces;
using PostgreSQL.Models;

namespace PostgreSQL.Access;

public class ConnectionAccess(PostgresDbContext dbContext) : IConnectionAccess
{
    public async Task<bool> AddConnection(int userId, string platform, string platformId)
    {
        await dbContext.Connections.AddAsync(new ConnectionEntity()
        {
            UserId = userId,
            Platform = platform,
            PlatformId = platformId
        });
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteConnection(int userId, string platform)
    {
        var connection = dbContext.Connections.FirstOrDefault(x => 
            x.Platform == platform
            && x.UserId == userId
        );
        if (connection == null) return false;
        dbContext.Connections.Remove(connection);
        return await dbContext.SaveChangesAsync() > 0;
    }
}
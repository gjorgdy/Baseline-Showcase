using Core.Interfaces;
using PostgreSQL.Models;

namespace PostgreSQL;

public class ConnectionAccess(User user, PostgresDbContext dbContext) : AConnectionAccess
{
    public override async Task<bool> AddConnection(string platform, string platformId)
    {
        await dbContext.Connections.AddAsync(new Connection()
        {
            Platform = platform,
            PlatformId = platformId,
            User = user
        });
        return await dbContext.SaveChangesAsync() > 0;
    }

    public override async Task<bool> DeleteConnection(string platform)
    {
        var connection = dbContext.Connections.FirstOrDefault(x => 
            x.Platform == platform
            && x.User == user
        );
        if (connection == null) return false;
        dbContext.Connections.Remove(connection);
        return await dbContext.SaveChangesAsync() > 0;
    }
}
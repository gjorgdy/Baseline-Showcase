using Core.Interfaces;

namespace PostgreSQL;

public class DataAccess(PostgresDbContext dbContext) : IDataAccess
{
    public AUserAccess? GetUserAccess(int userId)
    {
        var user = dbContext.Users.FirstOrDefault(user => user.Id == userId);
        return user == null ? null : new UserAccess(user, dbContext);
    }

    public AUserAccess? GetUserAccess(string platform, string platformId)
    {
        var user = dbContext.Users.FirstOrDefault(user => user.Connections.Any(
            connection => connection.Platform == platform 
                && connection.PlatformId == platformId
            )
        );
        return user == null ? null : new UserAccess(user, dbContext);
    }

    public IRoleAccess GetRoleAccess() => new RoleAccess(dbContext);
}
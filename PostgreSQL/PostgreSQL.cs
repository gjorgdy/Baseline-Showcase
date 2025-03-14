using Core.Interfaces;

namespace PostgreSQL;

public class PostgreSql(PostgresDbContext dbContext) : IDataAccess
{
    
    public int GetUserId(string platform, string platformId)
    {
        throw new NotImplementedException();
    }

    public string? GetUserDisplayName(int id)
    {
        return dbContext.Users
            .Where(u => u.Id == id)
            .Select(u => u.DisplayName)
            .FirstOrDefault();
    }
}
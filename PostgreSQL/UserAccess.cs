using Core.Interfaces;
using PostgreSQL.Models;

namespace PostgreSQL;

public class UserAccess(User user, PostgresDbContext dbContext) : AUserAccess
{
    
    public override int GetId() => user.Id;
    public override string GetDisplayName() => user.DisplayName;
    public override string GetProfilePicture() => user.ProfilePicture;

    public override bool SetDisplayName(string displayName) {
        user.DisplayName = displayName;
        return dbContext.SaveChanges() > 0;
    }

    public override bool SetProfilePicture(string picture)
    {
        user.ProfilePicture = picture;
        return dbContext.SaveChanges() > 0;
    }

    public override AConnectionAccess GetConnectionAccess() 
        => new ConnectionAccess(user, dbContext);

    public override ATileAccess GetTileAccess()
        => new TileAccess(user, dbContext);
}
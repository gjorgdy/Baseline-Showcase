using System.Text.Json;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using PostgreSQL.Models;

namespace PostgreSQL;

public class TileAccess(User user, PostgresDbContext dbContext) : ATileAccess
{
    public override async Task<Guid> AddTile(JsonDocument attributes)
    {
        // Add new tile
        var tile = dbContext.Tiles.Add(new Tile
        {
            Attributes = attributes,
            NextTile = null,
            User = user
        });
        // Link previous tile
        var previousTile = await dbContext.Tiles.FirstOrDefaultAsync(t => 
            t.NextTile == null 
            && t.User == user
        );
        if (previousTile != null) previousTile.NextTile = tile.Entity;
        // Commit changes
        await dbContext.SaveChangesAsync();
        return tile.Entity.Id;
    }

    public override Task<bool> UpdateTile(Guid id, JsonDocument tile)
    {
        throw new NotImplementedException();
    }

    public override Task<bool> MoveTile(Guid id, Guid afterTileId)
    {
        throw new NotImplementedException();
    }

    public override Task<bool> DeleteTile(Guid id)
    {
        throw new NotImplementedException();
    }
}
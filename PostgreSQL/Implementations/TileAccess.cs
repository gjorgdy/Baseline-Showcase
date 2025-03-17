using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using PostgreSQL.Models;

namespace PostgreSQL.Access;

public class TileAccess(PostgresDbContext dbContext) : ITileAccess
{
    public Task<List<TileModel>> GetTiles(int userId)
    {
        var tiles = dbContext.Tiles.Where(t => t.UserId == userId).ToList();
        return Task.FromResult(
            tiles
                .Select(t => t.GetModel())
                .Where(t => t != null)
                .Select(t => t!)
                .ToList()
        );
    }

    public async Task<Guid> AddTile(int userId, string attributeJson)
    {
        // Add new tile
        var tile = dbContext.Tiles.Add(new TileEntity
        {
            UserId = userId,
            Attributes = attributeJson,
            NextTile = null,
        });
        // Link previous tile
        var previousTile = await dbContext.Tiles.FirstOrDefaultAsync(t => 
            t.NextTile == null 
            && t.UserId == userId
        );
        if (previousTile != null) previousTile.NextTile = tile.Entity;
        // Commit changes
        await dbContext.SaveChangesAsync();
        return tile.Entity.Id;
    }

    public async Task<bool> UpdateTile(Guid id, string attributeJson)
    {
        var tile = await dbContext.Tiles.FirstOrDefaultAsync(t => t.Id == id);
        if (tile == null) return false;
        tile.Attributes = attributeJson;
        return await dbContext.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Move tile 'C' between tile 'A' and 'B'
    /// </summary>
    /// <returns></returns>
    public async Task<bool> MoveTile(Guid tileCId, Guid tileAId)
    {
        await RemoveLink(tileCId);
        // Tile C after A, before B
        var tileA = await dbContext.Tiles.FirstOrDefaultAsync(t => t.Id == tileAId);
        var tileC = await dbContext.Tiles.FirstOrDefaultAsync(t => t.Id == tileCId);
        if (tileA == null || tileC == null) return false;
        var tileBId = tileA.NextTileId;
        tileA.NextTileId = tileCId;
        tileC.NextTileId = tileBId;
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteTile(Guid id)
    {
        await RemoveLink(id);
        return await dbContext.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Remove a tile out of the linked list
    ///
    /// WARNING: Does not save changes
    /// </summary>
    /// <param name="tileId"></param>
    /// <returns></returns>
    private async Task<bool> RemoveLink(Guid tileId)
    {
        var tile = await dbContext.Tiles.FirstOrDefaultAsync(t => t.Id == tileId);
        var previousTile = await dbContext.Tiles.FirstOrDefaultAsync(t => t.NextTileId == tileId);
        // previousTile may be null if tile is first in linked list
        if (previousTile == null) return true;
        if (tile == null) return false;
        previousTile.NextTileId = tile.NextTileId;
        return true;
    }
}
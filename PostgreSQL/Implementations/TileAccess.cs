using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using PostgreSQL.Models;

namespace PostgreSQL.Implementations;

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

    private async Task<TileEntity?> GetTileEntity(int userId, Guid tileId)
    {
        return await dbContext.Tiles.FirstOrDefaultAsync(t => 
            t.Id == tileId
            && t.UserId == userId
        );
    }
    
    public async Task<TileModel?> GetTile(int userId, Guid tileId) {
        var tileEntity = await GetTileEntity(userId, tileId);
        return tileEntity?.GetModel();
    }

    public async Task<TileModel?> AddTile(int userId, string type, string attributeJson)
    {
        // Add new tile
        var tile = dbContext.Tiles.Add(new TileEntity
        {
            UserId = userId,
            Type = type,
            Attributes = attributeJson,
            NextTile = null,
            Width = 1,
            Height = 1
        });
        // Link previous tile
        var previousTile = await dbContext.Tiles.FirstOrDefaultAsync(t => 
            t.NextTile == null 
            && t.UserId == userId
        );
        if (previousTile != null) previousTile.NextTile = tile.Entity;
        // Commit changes
        await dbContext.SaveChangesAsync();
        return tile.Entity.GetModel();
    }

    public async Task<bool> UpdateTile(int userId, Guid id, string attributeJson)
    {
        var tile = await dbContext.Tiles.FirstOrDefaultAsync(t => t.Id == id);
        if (tile == null) throw new NullReferenceException();
        tile.Attributes = attributeJson;
        return await dbContext.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Move tile 'C' between tile 'A' and 'B'
    /// </summary>
    /// <returns></returns>
    public async Task<bool> MoveTile(int userId, Guid tileCId, Guid tileAId)
    {
        // Tile C after A, before B
        var tileA = await GetTileEntity(userId, tileAId);
        var tileC = await GetTileEntity(userId, tileCId);
        if (tileA == null || tileC == null) throw new NullReferenceException();
        await RemoveLink(tileC);
        var tileBId = tileA.NextTileId;
        tileA.NextTileId = tileCId;
        tileC.NextTileId = tileBId;
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteTile(int userId, Guid tileId)
    {
        var tile = await GetTileEntity(userId, tileId);
        if (tile == null) throw new NullReferenceException();
        await RemoveLink(tile);
        dbContext.Tiles.Remove(tile);
        return await dbContext.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Remove a tile out of the linked list
    ///
    /// WARNING: Does not save changes
    /// </summary>
    /// <param name="tile">The tile which needs to be removed from the linked list</param>
    /// <returns></returns>
    private async Task<bool> RemoveLink(TileEntity tile)
    {
        var previousTile = await dbContext.Tiles.FirstOrDefaultAsync(t => t.NextTileId == tile.Id);
        // previousTile may be null if tile is first in linked list
        if (previousTile == null) return true;
        previousTile.NextTileId = tile.NextTileId;
        return true;
    }
}
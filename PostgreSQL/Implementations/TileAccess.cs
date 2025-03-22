using System.Text.Json;
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

    public async Task<TileModel?> AddTile(int userId, TileContentModel tileModel)
    {
        // Add new tile
        var tile = dbContext.Tiles.Add(new TileEntity
        {
            UserId = userId,
            Type = tileModel.Type,
            Attributes = JsonSerializer.Serialize(
                tileModel.Attributes
            ),
            NextTile = null,
            Width = tileModel.Width,
            Height = tileModel.Height,
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

    public async Task<bool> UpdateTile(int userId, Guid tileId, TileContentModel tileModel)
    {
        var tile = await dbContext.Tiles.FirstOrDefaultAsync(t => t.Id == tileId);
        if (tile == null) throw new NullReferenceException();
        tile.Type = tileModel.Type;
        tile.Attributes = JsonSerializer.Serialize(
            tileModel.Attributes
        );
        tile.Width = tileModel.Width;
        tile.Height = tileModel.Height;
        return await dbContext.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Move tile 'C' between tile 'A' and 'B'
    /// </summary>
    /// <returns></returns>
    public async Task<bool> ReorderTiles(int userId, List<Guid> tileIds)
    {
        for (int i = 0; i < tileIds.Count; i++)
        {
            var tileA = await GetTileEntity(userId, tileIds[i]);
            if (tileA == null) throw new NullReferenceException();
            tileA.NextTileId = i == tileIds.Count - 1 ? null : tileIds[i + 1];
        }
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
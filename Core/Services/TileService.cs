using System.Text.Json;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class TileService(ITileAccess tileAccess)
{
    public Task<List<TileModel>> GetTiles(int userId) => tileAccess.GetTiles(userId);
    
    public async Task<TileModel?> AddTile(int userId, JsonDocument attributeJson)
    {
        // TODO : Validate the content
        if (attributeJson?.ToString() is null) throw new InvalidTileAttributesException();
        await Console.Out.WriteLineAsync(JsonSerializer.Serialize(attributeJson));
        var tileId = await tileAccess.AddTile(userId, JsonSerializer.Serialize(attributeJson));
        return await tileAccess.GetTile(tileId);
    }
    
    public Task<bool> UpdateTile(Guid id, string attributeJson) => tileAccess.UpdateTile(id, attributeJson);
    public Task<bool> MoveTile(Guid tileCId, Guid tileAId) => tileAccess.MoveTile(tileCId, tileAId);
    public Task<bool> DeleteTile(Guid id) => tileAccess.DeleteTile(id);
}
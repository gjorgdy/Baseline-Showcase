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
        string attributeJsonString = ValidateTile(attributeJson);
        return await tileAccess.AddTile(userId, attributeJsonString);
    }
    
    public async Task<bool> UpdateTile(int userId, Guid id, JsonDocument attributeJson) {
        string attributeJsonString = ValidateTile(attributeJson);
        return await tileAccess.UpdateTile(userId, id, attributeJsonString);
    }
    public Task<bool> MoveTile(int userId, Guid tileCId, Guid tileAId) => tileAccess.MoveTile(userId, tileCId, tileAId);
    public Task<bool> DeleteTile(int userId, Guid id) => tileAccess.DeleteTile(userId, id);

    public string ValidateTile(JsonDocument attributeJson)
    {
        // TODO : Validate the content
        if (attributeJson?.ToString() is null) throw new InvalidTileAttributesException();
        return JsonSerializer.Serialize(attributeJson);
    }
    
}
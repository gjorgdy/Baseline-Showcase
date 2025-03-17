using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class TileService(ITileAccess tileAccess) : ITileAccess
{
    public Task<List<TileModel>> GetTiles(int userId) => tileAccess.GetTiles(userId);
    public Task<Guid> AddTile(int userId, string attributeJson) => tileAccess.AddTile(userId, attributeJson);
    public Task<bool> UpdateTile(Guid id, string attributeJson) => tileAccess.UpdateTile(id, attributeJson);
    public Task<bool> MoveTile(Guid tileCId, Guid tileAId) => tileAccess.MoveTile(tileCId, tileAId);
    public Task<bool> DeleteTile(Guid id) => tileAccess.DeleteTile(id);
}
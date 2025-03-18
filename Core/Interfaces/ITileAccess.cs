using Core.Models;

namespace Core.Interfaces;

public interface ITileAccess
{
    public Task<List<TileModel>> GetTiles(int userId);
    public Task<TileModel?> GetTile(int userId, Guid tileId);
    public Task<TileModel?> AddTile(int userId, string attributeJson);
    public Task<bool> UpdateTile(int userId, Guid id, string attributeJson);
    public Task<bool> MoveTile(int userId, Guid tileCId, Guid tileAId);
    public Task<bool> DeleteTile(int userId, Guid id);
}
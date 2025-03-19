using Core.Models;

namespace Core.Interfaces;

public interface ITileAccess
{
    public Task<List<TileModel>> GetTiles(int userId);
    public Task<TileModel?> GetTile(int userId, Guid tileId);
    public Task<TileModel?> AddTile(int userId, string type, string attributeJson);
    public Task<bool> UpdateTile(int userId, Guid id, string attributeJson);
    public Task<bool> DeleteTile(int userId, Guid id);
    public Task<bool> ReorderTiles(int userId, List<Guid> tileIds);
}
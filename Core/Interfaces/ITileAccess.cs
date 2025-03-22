using Core.Models;

namespace Core.Interfaces;

public interface ITileAccess
{
    public Task<List<TileModel>> GetTiles(int userId);
    public Task<TileModel?> GetTile(int userId, Guid tileId);
    public Task<TileModel?> AddTile(int userId, TileContentModel tileContentModel);
    public Task<bool> UpdateTile(int userId, Guid tileId, TileContentModel tileContentModel);
    public Task<bool> DeleteTile(int userId, Guid id);
    public Task<bool> ReorderTiles(int userId, List<Guid> tileIds);
}
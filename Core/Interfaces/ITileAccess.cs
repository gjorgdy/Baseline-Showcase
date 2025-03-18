using Core.Models;

namespace Core.Interfaces;

public interface ITileAccess
{
    public Task<List<TileModel>> GetTiles(int userId);
    public Task<TileModel?> GetTile(Guid tileId);
    public Task<Guid> AddTile(int userId, string attributeJson);
    public Task<bool> UpdateTile(Guid id, string attributeJson);
    public Task<bool> MoveTile(Guid tileCId, Guid tileAId);
    public Task<bool> DeleteTile(Guid id);
}
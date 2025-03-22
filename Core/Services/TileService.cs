using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class TileService(ITileAccess tileAccess)
{
    public Task<TileModel?> GetTile(int userId, Guid tileId) => tileAccess.GetTile(userId, tileId);
    public Task<List<TileModel>> GetTiles(int userId) => tileAccess.GetTiles(userId);
    public async Task<TileModel?> AddTile(int userId, TileContentModel tileContentModel) {
        tileContentModel.ValidateOrThrow();
        return await tileAccess.AddTile(userId, tileContentModel);
    }
    public async Task<bool> UpdateTile(int userId, Guid tileId, TileContentModel tileContentModel) {
        tileContentModel.ValidateOrThrow();
        return await tileAccess.UpdateTile(userId, tileId, tileContentModel);
    }
    public Task<bool> ReorderTiles(int userId, List<Guid> tileIds) => tileAccess.ReorderTiles(userId, tileIds);
    public Task<bool> DeleteTile(int userId, Guid id) => tileAccess.DeleteTile(userId, id);
}
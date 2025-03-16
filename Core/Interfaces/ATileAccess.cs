using System.Text.Json;

namespace Core.Interfaces;

public abstract class ATileAccess
{
    public abstract Task<Guid> AddTile(JsonDocument tile);
    public abstract Task<bool> UpdateTile(Guid id, JsonDocument tile);
    public abstract Task<bool> MoveTile(Guid id, Guid afterTileId);
    public abstract Task<bool> DeleteTile(Guid id);
}
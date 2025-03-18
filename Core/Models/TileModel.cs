using System.Text.Json;

namespace Core.Models;

public record TileModel(Guid Id, JsonDocument Attributes, Guid? NextTileId);
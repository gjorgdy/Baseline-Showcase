using System.Text.Json;

namespace Core.Models;

public record TileModel(Guid Id, string Type, JsonDocument Attributes, Guid? NextTileId, int Width, int Height);
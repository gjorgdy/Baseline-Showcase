using System.Text.Json;
using System.Text.Json.Nodes;

namespace Core.Models;

public record TileModel(
    Guid Id, 
    string Type, 
    JsonDocument Attributes, 
    int Width, 
    int Height, 
    Guid? NextTileId = null
    ) : TileContentModel(Type, Attributes, Width, Height);
using System.Text.Json;

namespace ApiServer.Model;

public record PutTileForm(string Type, int Width, int Height, JsonDocument Attributes);
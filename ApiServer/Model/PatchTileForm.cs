using System.Text.Json;

namespace ApiServer.Model;

public record PatchTileForm(int Width, int Height, JsonDocument Attributes);
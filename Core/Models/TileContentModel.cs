using System.Text.Json;
using System.Text.Json.Nodes;
using Core.Exceptions;
using Core.Models.Tiles;

namespace Core.Models;

public record TileContentModel(string Type, JsonDocument Attributes, int Width = 1, int Height = 1)
{

    private static readonly string[] Types = ["paragraph", "skills"];
    
    public void ValidateOrThrow()
    {
        if (!Types.Contains(Type))
        {
            throw new InvalidTileTypeException(Type);
        }

        if (Width is < 1 or > 3)
        {
            throw new InvalidTileSizeException("Tile width must be between 1 and 3.");
        }

        if (Height is < 1 or > 3)
        {
            throw new InvalidTileSizeException("Tile height must be between 1 and 3.");
        }

        switch (Type)
        {
            case "paragraph":
                ParagraphTileModel.FromJson(Attributes).ValidateOrThrow();
                break;
            case "skills":
                SkillsTileModel.FromJson(Attributes).ValidateOrThrow();
                break;
        }
    }
    
}
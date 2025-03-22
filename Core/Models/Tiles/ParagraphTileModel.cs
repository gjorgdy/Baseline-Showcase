using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Core.Exceptions;
using Newtonsoft.Json;

namespace Core.Models.Tiles;

// ReSharper disable once ClassNeverInstantiated.Global
public class ParagraphTileModel {
    
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("paragraph")]
    public string? Paragraph { get; set; }
    
    public static ParagraphTileModel FromJson(JsonDocument jsonDocument)
    {
        var model = jsonDocument.Deserialize<ParagraphTileModel>();
        if (model == null) throw new InvalidTileAttributesException("Not all properties are defined");
        return model;
    }
    
    public void ValidateOrThrow()
    {
        
        if (Title == null || Title.Length is 0 or > 32)
        {
            throw new InvalidTileAttributesException("Title must be between 1 and 32 characters long.");
        }
        
        if (Paragraph == null || Paragraph.Length is 0 or > 256)
        {
            throw new InvalidTileAttributesException("Paragraph must be between 1 and 256 characters long.");
        }
        
    }
    
}
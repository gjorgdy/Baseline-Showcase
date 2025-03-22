using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Core.Exceptions;
using Newtonsoft.Json;

namespace Core.Models.Tiles;

// ReSharper disable once ClassNeverInstantiated.Global
public class SkillsTileModel {
    
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("skills")]
    public Skill[]? Skills { get; set; }
    
    public static SkillsTileModel FromJson(JsonDocument jsonDocument)
    {
        var model = jsonDocument.Deserialize<SkillsTileModel>();
        if (model == null) throw new InvalidTileAttributesException("Not all properties are defined");
        return model;
    }
    
    public void ValidateOrThrow()
    {
        
        if (Title == null || Title.Length is 0 or > 32)
        {
            throw new InvalidTileAttributesException("Title must be between 1 and 32 characters long.");
        }

        if (Skills == null)
        {
            throw new InvalidTileAttributesException("Skills can not be empty.");
        }
        
        foreach (var skill in Skills)
        {
            if (skill.Name == null || skill.Name.Length is < 1 or > 32)
            {
                throw new InvalidTileAttributesException("Skill names must be between 1 and 32 characters long.");
            }
            
            if (skill.Percentage is < 0 or > 100)
            {
                throw new InvalidTileAttributesException("Skill percentage must be between 0 and 100.");
            }
        }

        if (Skills.Length == 0)
        {
            throw new InvalidTileAttributesException("Skills can not be empty.");
        }
        
    }
    
    public class Skill
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("percentage")]
        public int? Percentage { get; set; }
    }
    
}
using System.Text.Json;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Moq;
using static NUnit.Framework.Assert;

namespace Tests.Services;

public class TileTest
{
    TileService _tileService;
    
    [SetUp]
    public void Setup()
    {
        Mock<ITileAccess> mockTileAccess = new();
        mockTileAccess
            .Setup(tileAccess => 
                tileAccess.AddTile(It.IsAny<int>(), It.IsAny<TileContentModel>())
            ).Returns((int id, TileContentModel tileContentModel) => 
                Task.FromResult<TileModel?>(
                        new TileModel(
                        Guid.Empty, 
                        tileContentModel.Type,
                        tileContentModel.Attributes,
                        tileContentModel.Width,
                        tileContentModel.Height
                    )
                )
            );
        _tileService = new TileService(mockTileAccess.Object);
    }

    [Test]
    public void ValidParagraphTile()
    {
        var validTile = new TileContentModel(
            "paragraph", 
            JsonDocument.Parse("""
               {
                  "title" : "valid title",
                  "paragraph" : "this is a valid input for a paragraph"
               }
            """));
        DoesNotThrowAsync(async () =>
        {
            await _tileService.AddTile(0, validTile);
        });
    }

    [Test]
    public void InvalidParagraphTile()
    {
        var validTile = new TileContentModel(
            "paragraph", 
            JsonDocument.Parse("""
                                  {
                                    "invalid_key" : "and an invalid value"
                                  }
                               """));
        ThrowsAsync<InvalidTileAttributesException>(async () =>
        {
            await _tileService.AddTile(0, validTile);
        });
    }

    [Test]
    public void ValidSkillsTile()
    {
        var validTile = new TileContentModel(
            "skills", 
            JsonDocument.Parse("""
                                  {
                                     "title" : "valid title",
                                     "skills" : [
                                        {
                                          "name" : "skill name",
                                          "percentage": 10
                                        }
                                     ]
                                  }
                               """));
        DoesNotThrowAsync(async () =>
        {
            await _tileService.AddTile(0, validTile);
        });
    }

    [Test]
    public void InvalidSkillsTile()
    {
        var validTile = new TileContentModel(
            "skills", 
            JsonDocument.Parse("""
                                  {
                                     "title" : "valid title",
                                     "skills" : [
                                        {
                                          "name" : "too high",
                                          "percentage": 102
                                        }
                                     ]
                                  }
                               """));
        ThrowsAsync<InvalidTileAttributesException>(async () =>
        {
            await _tileService.AddTile(0, validTile);
        });
    }
    
}
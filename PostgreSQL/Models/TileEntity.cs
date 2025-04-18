﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Nodes;
using Core.Exceptions;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace PostgreSQL.Models;

[PrimaryKey("Id")]
public class TileEntity
{

    [DefaultValue("uuid_generate_v4()")]
    public Guid Id { get; set; }
    
    public required string Type { get; set; }
    
    [Column(TypeName = "jsonb")]
    public required string Attributes { get; set; }
    
    public required int Width { get; set; }
    
    public required int Height { get; set; }
    
    // Linked list reference
    [ForeignKey("Id")]
    public Guid? NextTileId { get; set; }
    
    public TileEntity? NextTile { get; set; }
    
    // Owner Reference
    [ForeignKey("Id")]
    public int UserId { get; set; }
    
    public UserEntity? User { get; set; }

    public TileModel? GetModel()
    {
        try
        {
            var json = JsonDocument.Parse(Attributes);
            return new TileModel(
                Id, 
                Type, 
                json, 
                Width, 
                Height, 
                NextTileId
            );
        }
        catch (Exception e)
        {
            throw new TileException("'Tile could not be parsed");
        }
    }
}
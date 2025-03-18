using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace PostgreSQL.Models;

[PrimaryKey("Id")]
public class TileEntity
{

    [DefaultValue("uuid_generate_v4()")]
    public Guid Id { get; set; }
    
    [Column(TypeName = "jsonb")]
    public required string Attributes { get; set; }
    
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
            return new TileModel(Id, JsonDocument.Parse(Attributes), NextTileId);
        }
        catch (Exception e)
        {
            // TODO: Log
            return null;
        }
    }
}
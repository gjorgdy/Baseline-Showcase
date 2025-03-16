using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace PostgreSQL.Models;

[PrimaryKey("Id")]
public class Tile
{
    [ForeignKey("Id")]
    public int UserId { get; set; }

    [DefaultValue("uuid_generate_v4()")]
    public Guid Id { get; set; }

    [ForeignKey("Id")]
    public Guid? NextId { get; set; }

    public JsonDocument Attributes { get; set; } = JsonDocument.Parse("{}");
    
    public Tile? NextTile { get; set; }
    
    public User? User { get; set; }
}
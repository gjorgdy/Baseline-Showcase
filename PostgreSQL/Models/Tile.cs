using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    [NpgsqlTypes.PgName("Jsonb")]
    public required string Attributes { get; set; }
    
    public required Tile NextTile { get; set; }
    
    public required User User { get; set; }
}
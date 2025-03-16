using System.ComponentModel.DataAnnotations;

namespace PostgreSQL.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(32)]
    public required string DisplayName { get; set; }
    
    [StringLength(32)]
    public required string ProfilePicture { get; set; }

    public required ICollection<Tile> Tiles { get; init; }
    
    public required ICollection<Role> Roles { get; init; }
    
    public required ICollection<Connection> Connections { get; init; }
}
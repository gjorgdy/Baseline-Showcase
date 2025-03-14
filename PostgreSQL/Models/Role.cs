using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PostgreSQL.Models;

[PrimaryKey("Name")]
public class Role
{
    
    [StringLength(16)]
    public required string Name { get; set; }
    
    [StringLength(16)]
    public required string DisplayName { get; set; }
    
    [StringLength(16)]
    public required string Platform { get; set; }
    
    public required ICollection<User> Users { get; init; }
    
}
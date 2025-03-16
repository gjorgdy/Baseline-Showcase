using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PostgreSQL.Models;

[PrimaryKey("Id")]
public class Role
{
    
    [StringLength(16)]
    public required string Id { get; set; }
    
    [StringLength(16)]
    public required string DisplayName { get; set; }
    
    [StringLength(16)]
    public required string Platform { get; set; }

    public ICollection<User> Users { get; init; } = [];

}
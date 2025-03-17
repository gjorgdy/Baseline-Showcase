using System.ComponentModel.DataAnnotations;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace PostgreSQL.Models;

[PrimaryKey("Id")]
public class RoleEntity
{
    
    [StringLength(16)]
    public required string Id { get; set; }
    
    [StringLength(16)]
    public required string DisplayName { get; set; }
    
    [StringLength(16)]
    public required string Platform { get; set; }

    public ICollection<UserEntity> Users { get; init; } = [];

    public RoleModel GetModel() => new(Id, DisplayName, Platform);

}
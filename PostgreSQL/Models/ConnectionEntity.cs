using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace PostgreSQL.Models;

[PrimaryKey("UserId", ["Platform"])]
public class ConnectionEntity
{

    [ForeignKey("Id")]
    public int UserId { get; set; }
    
    [StringLength(16)]
    public required string Platform { get; set; }
    
    [StringLength(128)]
    public required string PlatformId { get; set; }
    
    public UserEntity UserEntity { get; init; }
    
    public ConnectionModel GetModel() => new(UserId, Platform, PlatformId);
    
}
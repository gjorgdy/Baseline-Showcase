using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PostgreSQL.Models;

[PrimaryKey("UserId", ["Platform"])]
public class Connection
{

    [ForeignKey("Id")]
    public int UserId { get; set; }
    
    [StringLength(16)]
    public required string Platform { get; set; }
    
    [StringLength(128)]
    public required string PlatformId { get; set; }
    
    [Required]
    public required User User { get; init; }
}
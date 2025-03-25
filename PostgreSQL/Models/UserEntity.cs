using System.ComponentModel.DataAnnotations;
using Core.Enums;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using static System.Enum;

namespace PostgreSQL.Models;

public class UserEntity
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(32)]
    public required string DisplayName { get; set; }
    
    [StringLength(32)]
    public required string ProfilePicture { get; set; }
    
    [Required]
    public ICollection<TileEntity> Tiles { get; set; } = [];

    [Required]
    public ICollection<RoleEntity> Roles { get; set; } = [];
    
    [Required]
    public ICollection<ConnectionEntity> Connections { get; set; } = [];
    
    public UserData? GetDataModel()
    {
        bool validDn = TryParse<DisplayNamePlatform>(DisplayName, true, out var displayName);
        bool validPfp = TryParse<ProfilePicturePlatform>(ProfilePicture, true, out var profilePicture);
        if (validDn && validPfp)
            return new UserData(
                Id,
                DisplayName,
                ProfilePicture,
                Connections.Select(c => c.GetModel()).ToList(),
                Roles.Select(c => c.GetModel()).ToList()
            );
        return null;
    }
    
}
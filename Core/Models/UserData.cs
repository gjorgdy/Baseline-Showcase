namespace Core.Models;

public record UserData (
    int Id,
    string DisplayNamePlatform, 
    string ProfilePicturePlatform,
    List<ConnectionModel> Connections,
    List<RoleModel> Roles
);
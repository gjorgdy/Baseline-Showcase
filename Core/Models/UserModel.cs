using Core.Enums;

namespace Core.Models;

public record UserModel(
    int Id, 
    string DisplayName, 
    string ProfilePicture
);
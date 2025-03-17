namespace Core.Models;

public record ProfileModel(UserModel User, IEnumerable<TileModel> Tiles);
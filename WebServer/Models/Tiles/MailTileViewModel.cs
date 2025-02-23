namespace Core.Models.Tiles;

public class MailTileViewModel(string title, int width, int height, int userId) : TileViewModel(title, width, height)
{

    public int UserId { get; } = userId;

}
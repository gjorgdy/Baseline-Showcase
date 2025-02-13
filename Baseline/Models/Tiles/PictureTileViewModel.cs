namespace Baseline.Models.Tiles;

public class PictureTileViewModel(string title, int width, int height, string imageUrl) : TileViewModel(title, width, height)
{
    public string ImageUrl => imageUrl;
}
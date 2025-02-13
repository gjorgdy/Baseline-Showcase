namespace Baseline.Models.Tiles;

public class NameTileViewModel(string title, int width, int height, string name) : TileViewModel(title, width, height)
{
    public string Name => name;
}
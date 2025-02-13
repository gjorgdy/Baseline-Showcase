namespace Baseline.Models.Tiles;

public abstract class TileViewModel(string title, int width, int height)
{
    public string Title { get; } = title;
    public int Width { get; } = width;
    public int Height { get; } = height;

    public string width()
    {
        return Width switch
        {
            1 => "one-wide",
            2 => "two-wide",
            3 => "three-wide",
            _ => ""
        };
    }

    public string height()
    {
        return Height switch
        {
            1 => "one-high",
            2 => "two-high",
            3 => "three-high",
            _ => ""
        };
    }

}
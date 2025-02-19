namespace Baseline.Models.Tiles;

public class ParagraphTileViewModel(string title, int width, int height, string paragraph) : TileViewModel(title, width, height)
{
    public string Paragraph => paragraph;
}
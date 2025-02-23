using Core.Models.Tiles;

namespace Core.Models;

public class GridViewModel(List<TileViewModel> tiles)
{
    public List<TileViewModel> Tiles { get; } = tiles;
}
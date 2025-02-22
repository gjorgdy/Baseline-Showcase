using Baseline.Models.Tiles;

namespace Baseline.Models;

public class GridViewModel(List<TileViewModel> tiles)
{
    public List<TileViewModel> Tiles { get; } = tiles;
}
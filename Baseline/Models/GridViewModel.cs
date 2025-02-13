using Baseline.Models.Tiles;

namespace Baseline.Models;

public class GridViewModel(List<TileViewModel> tileViewModels)
{
    public List<TileViewModel> TileViewModels { get; } = tileViewModels;
}
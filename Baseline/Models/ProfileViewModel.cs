using Baseline.Models.Tiles;

namespace Baseline.Models;

public class ProfileViewModel(int userId, List<GridViewModel>? grids = null)
{

    public int UserId { get; } = userId;

    public List<GridViewModel> Grids { get; } = grids ?? [];

    public ProfileViewModel AddTile(int gridIndex, TileViewModel tile)
    {
        if (gridIndex == Grids.Count)
        {
            Grids.Add(new GridViewModel([]));
        }
        var grid = Grids[gridIndex];
        grid.Tiles.Add(tile);
        return this;
    }

}
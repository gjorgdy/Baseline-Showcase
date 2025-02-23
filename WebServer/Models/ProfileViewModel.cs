using Core.Models.Tiles;

namespace Core.Models;

public class ProfileViewModel(string displayName, int userId, List<GridViewModel>? grids = null)
{

    public string DisplayName { get; } = displayName;

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
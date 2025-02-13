namespace Baseline.Models.Tiles;

// TODO: Use an API to retrieve this information
public class SpotifyTileViewModel(
    string title,
    int width,
    int height,
    string playlistUrl
) : TileViewModel(title, width, height)
{
    public string PlaylistUrl => playlistUrl;

    public string ImageUrl => "https://image-cdn-ak.spotifycdn.com/image/ab67706c0000da84cf8957391f0fedbc57fb1963";

    public string Title => "a good name";

    public int Size = 875;

}
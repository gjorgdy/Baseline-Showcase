using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Baseline.Models;
using Baseline.Models.Tiles;

namespace Baseline.Controllers;

public class ProfileController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        return View(new GridViewModel(
            [
                // Profile Picture
                new PictureTileViewModel(
                    "Profile Picture",
                    1,
                    1,
                    "/resources/pfp.png"
                ),
                new NameTileViewModel(
                    "Display Name",
                    2,
                    1,
                    "Jordy"
                ),
                // Education
                new ExperiencesTileViewModel(
                    "Opleidingen",
                    2,
                    2
                ).add(
                    "HBO-ICT",
                    "2023 - Nu",
                    "Hogeschool Windesheim Zwolle"
                ).add(
                    "Atheneum",
                    "2015 - 2022",
                    "Vechtdal College Hardenberg"
                ),
                // Languages
                new SkillsTileViewModel(
                    "Talen",
                    1,
                    2
                ).add(
                    "Nederlands",
                    100
                ).add(
                    "Engels",
                    100
                ).add(
                    "Duits",
                    25
                ),
                // Spotify
                new SpotifyTileViewModel(
                    "My Playlist",
                    1,
                    2,
                    "https://open.spotify.com/playlist/27Byq479ZJuUzGDagJfMD9?si=3f8838236bf94da9"
                ),
                // Jobs
                new ExperiencesTileViewModel(
                    "Ervaring",
                    2,
                    1
                ).add(
                    "Kassamedewerker",
                    "2019 - 2022",
                    "Lidl Hardenberg"
                )
            ]
        ));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
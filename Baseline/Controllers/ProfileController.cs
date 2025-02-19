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
                    "/resources/pfp.jpg"
                ),
                new NameTileViewModel(
                    "Display Name",
                    2,
                    1,
                    "Jordy"
                ),
                // Description
                new ParagraphTileViewModel(
                    "Biography",
                    3,
                    1,
                    """
                        I am a student currently studying software development. 
                        I like to develop game mods at home, especially for Minecraft.
                        Besides programming, my hobbies are gaming, video editing, and graphic design.
                    """
                ),
                // Languages
                new SkillsTileViewModel(
                    "Languages",
                    1,
                    1
                ).add(
                    "Dutch",
                    100
                ).add(
                    "English",
                    100
                ).add(
                    "German",
                    25
                ),
                // Education
                new ExperiencesTileViewModel(
                    "Studies",
                    2,
                    1
                ).add(
                    "HBO-ICT",
                    "2023 - Now",
                    "University Windesheim Zwolle"
                ).add(
                    "Atheneum",
                    "2015 - 2022",
                    "Vechtdal College Hardenberg"
                ),
                // Jobs
                new ExperiencesTileViewModel(
                    "Experiences",
                    2,
                    1
                ).add(
                    "Cassiere",
                    "2019 - 2022",
                    "Lidl Hardenberg"
                ),
                // Programming Languages
                new SkillsTileViewModel(
                    "Programming Languages",
                    1,
                    2
                ).add(
                    "Java",
                    75
                ).add(
                    "C#",
                    70
                ).add(
                    "Python",
                    50
                ).add(
                    "Javascript",
                    25
                ).add(
                    "GoLang",
                    10
                ),
                // Spotify
                new SpotifyTileViewModel(
                    "My Playlist",
                    1,
                    1,
                    "https://open.spotify.com/playlist/27Byq479ZJuUzGDagJfMD9?si=3f8838236bf94da9"
                ),
            ]
        ));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
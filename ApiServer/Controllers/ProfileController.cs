using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers;

[Route("users/{id:int}/profile")]
public class ProfileController(TileService tileService, UserService userService) : AAuthController
{

    [HttpGet("")]
    public async Task<IActionResult> GetProfile(int id)
    {
        var user = await userService.GetUser(id);
        var tiles = await tileService.GetTiles(id);
        if (user != null)
            return Ok(
                new ProfileModel(
                    user,
                    tiles
                )
            );
        return NotFound();
    }

    [HttpPut("")]
    public IActionResult PutTile(int id, [FromBody] int? targetIndex, [FromBody] object? tile)
    {
        int? userId = ValidateToken();
        return Ok(userId ?? -1);
    }

    [HttpPatch("{index:int}")]
    public IActionResult PatchTile(int id, int index, [FromBody] int? targetIndex, [FromBody] object? tile)
    {
        return Ok($"Update tile at index {index}, moved to index {targetIndex ?? -1} \n {tile}");
    }

    [HttpDelete("{index:int}")]
    public IActionResult PatchTile(int id, int index)
    {
        return Ok($"Deleted tile at index {index}");
    }

}
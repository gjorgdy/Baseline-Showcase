using System.Security.Claims;
using System.Text.Json;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ApiServer.Controllers;

[Route("users/{id:int}/profile")]
public class ProfileController(TileService tileService, UserService userService) : Controller
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
    [Authorize]
    public async Task<IActionResult> PutTile(int id, [FromBody] JsonDocument tileAttributes)
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        int userId = int.Parse(claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        await Console.Out.WriteLineAsync($"User's id: {userId}");
        // If client tries to modify another profile
        if (userId != id) return Forbid();
        // Update tile
        var tile = await tileService.AddTile(id, tileAttributes);
        if (tile != null) return Ok(tile);
        else return NotFound();
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
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PostgreSQL;

namespace ApiServer.Controllers;

[Route("users/{id:int}/profile")]
public class ProfileController(DataAccess dataAccess) : Controller
{

    [HttpGet("")]
    public IActionResult GetProfile(int id, int? tileStartIndex, int? tileEndIndex)
    { 
        string? name = dataAccess.GetUserAccess(id)?.GetDisplayName();
        return Ok(
           new List<string>
           {
               name ?? "no name",
           }
        );
    }

    [HttpPut("")]
    public IActionResult PutTile(int id, [FromBody] int? targetIndex, [FromBody] object? tile)
    {
        return Ok(id);
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
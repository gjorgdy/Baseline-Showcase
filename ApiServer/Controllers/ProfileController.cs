using System.Security.Claims;
using ApiServer.Model;
using Core.Authentication;
using Core.Exceptions;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers;

[Route("users/{id:int}/profile")]
public class ProfileController(TileService tileService, UserService userService) : Controller
{

    [HttpGet("")]
    public async Task<IActionResult> GetProfile(int id)
    {
        int? loggedInUserId = JwtTokenHandler.GetUserId(User);
        var user = await userService.GetUser(id);
        var tiles = await tileService.GetTiles(id);
        if (user != null) return Ok(new ProfileModel(user, tiles, loggedInUserId == user.Id));
        return NotFound();
    }

    [HttpGet("{tileId:Guid}")]
    public async Task<IActionResult> GetTile(int id, Guid tileId)
    {
        var tile = await tileService.GetTile(id, tileId);
        return tile == null ? NotFound() : Ok(tile);
    }

    [HttpPut("")]
    [Authorize]
    public async Task<IActionResult> PutTile(int id, [FromBody] TileContentModel? body)
    {
        if (body == null) return BadRequest("Tile sizes cannot be null");
        int? userId = JwtTokenHandler.GetUserId(User);
        // If client tries to modify another profile
        if (userId != id) return Forbid();
        // Add tile
        TileModel? tile;
        try
        {
            tile = await tileService.AddTile(id, body);
        }
        catch (TileException e)
        {
            return BadRequest(e.Message);
        }
        if (tile != null) return Ok(tile);
        return BadRequest();
    }

    [HttpPatch("{tileId:Guid}")]
    [Authorize]
    public async Task<IActionResult> PatchTile(int id, Guid tileId, [FromBody] TileContentModel? body)
    {
        if (body == null) return BadRequest("Tile sizes cannot be null");
        int? userId = JwtTokenHandler.GetUserId(User);
        // If client tries to modify another profile
        if (userId != id) return Forbid();
        // Update tile
        bool status;
        try
        {
            status = await tileService.UpdateTile(id, tileId, body);
        }
        catch (TileException e)
        {
            return BadRequest(e.Message);
        }
        return status ? Ok() : NotFound();
    }

    [HttpDelete("{tileId:Guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteTile(int id, Guid tileId)
    {
        int? userId = JwtTokenHandler.GetUserId(User);
        // If client tries to modify another profile
        if (userId != id) return Forbid();
        // Update tile
        bool status;
        try
        {
            status = await tileService.DeleteTile(id, tileId);
        }
        catch (InvalidTileAttributesException e)
        {
            return BadRequest(e.Message);
        }
        return status ? Ok() : NotFound();
    }

    [HttpPatch("")]
    [Authorize]
    public async Task<IActionResult> ReorderTile(int id, [FromBody] ReorderForm order)
    {
        int? userId = JwtTokenHandler.GetUserId(User);
        // If client tries to modify another profile
        if (userId != id) return Forbid();
        // Update tile
        bool status;
        try
        {
            status = await tileService.ReorderTiles(id, order.Order);
        }
        catch (InvalidTileAttributesException e)
        {
            return BadRequest(e.Message);
        }
        return status ? Ok(order.Order) : NotFound();
    }

}
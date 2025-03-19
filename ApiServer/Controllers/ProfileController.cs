﻿using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Nodes;
using ApiServer.Model;
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
        var user = await userService.GetUser(id);
        var tiles = await tileService.GetTiles(id);
        if (user != null) return Ok(new ProfileModel(user, tiles));
        return NotFound();
    }

    [HttpPut("")]
    [Authorize]
    public async Task<IActionResult> PutTile(int id, [FromBody] PutTileForm body)
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        int userId = int.Parse(claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        // If client tries to modify another profile
        if (userId != id) return Forbid();
        // Add tile
        var json = body.Attributes.Deserialize<JsonNode>();
        if (json is null) return BadRequest();
        var tile = await tileService.AddTile(id, body.Type, json);
        if (tile != null) return Ok(tile);
        else return NotFound();
    }

    [HttpPatch("{tileId:Guid}")]
    [Authorize]
    public async Task<IActionResult> PatchTile(int id, Guid tileId, [FromBody] PatchTileForm body)
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        int userId = int.Parse(claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        // If client tries to modify another profile
        if (userId != id) return Forbid();
        // Update tile
        bool status;
        try
        {
            var json = body.Attributes.Deserialize<JsonNode>();
            if (json is null) return BadRequest();
            status = await tileService.UpdateTile(id, tileId, json);
        }
        catch (InvalidTileAttributesException e)
        {
            return BadRequest(e.Message);
        }
        return status ? Ok() : NotFound();
    }

    [HttpDelete("{tileId:Guid}")]
    public async Task<IActionResult> PatchTile(int id, Guid tileId)
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        int userId = int.Parse(claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
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
    public async Task<IActionResult> ReorderTile(int id, [FromBody] ReorderForm order)
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        int userId = int.Parse(claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        // If client tries to modify another profile
        if (userId != id) return Forbid();
        // Update tile
        bool status;
        try
        {
            foreach (var guid in order.Order)
            {
                await Console.Out.WriteLineAsync(guid.ToString());
            }
            status = await tileService.ReorderTiles(id, order.Order);
        }
        catch (InvalidTileAttributesException e)
        {
            return BadRequest(e.Message);
        }
        return status ? Ok() : NotFound();
    }

}
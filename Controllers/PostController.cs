using Microsoft.AspNetCore.Mvc;
using SOCIALIZE.DTOs;
using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService) => _postService = postService;

    [HttpGet]
    public async Task<IActionResult> GetFeed()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User ID not found in token.");
        }

        var feed = await _postService.GetFeedAsync(userId);

        return feed == null ? NotFound("Feed is empty or user not found") : Ok(feed);

    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePostDTO dto)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        var post = await _postService.CreatePostAsync(dto, null);

        return Ok(post);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] int id)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var userRole = User.FindFirst(ClaimTypes.Role)!.Value;

        var success = await _postService.DeletePostAsync(userId, userRole, id);
        return success ? NoContent() : Forbid();
    }

    [HttpGet("api/[controller]")]
    public async Task<IActionResult> Update([FromBody] updatePostDTO dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var result = await _postService.UpdatePostAsync(userId, dto);

        return result ? Ok(dto) : Unauthorized("Cannot edit post!");
    }


}
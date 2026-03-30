using Microsoft.AspNetCore.Mvc;
using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;
using System.Security.Claims;
using SOCIALIZE.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace SOCIALIZE.Controllers
{
    [ApiController] 
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService) => _commentService = commentService;

        [HttpPost("api/[controller]")]
        public async Task<IActionResult> AddComment(createCommentDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
       
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var comment = await _commentService.AddCommentAsync(userId, dto.PostId, dto.Content, dto.parentId);

            return comment != null ? Ok(comment) : NotFound("Post not found");
        }

        [HttpDelete("api/[controller]")]
        public async Task<IActionResult> Delete(int id) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var success = await _commentService.DeleteCommentAsync(userId, userRole, id);

            return success ? NoContent() : Forbid();
        }

        [HttpPut("api/[controller]")]
        public async Task<IActionResult> Update(updateCommentDTO dto) 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            var success = await _commentService.UpdateCommentAsync(userId,dto);

            return success? Ok(dto):NotFound();
        }

        [HttpGet("api/[controller]")]
        public async Task<IActionResult> GetPostComments(int postId) 
        {
            if (postId == null) return NotFound("postId not found");
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var comments=await _commentService.GetCommentsByPostIdAsync(postId);

            if (comments.IsNullOrEmpty()) 
            {
                return NoContent();
            }

            return Ok(comments);
        }

       

    }
}
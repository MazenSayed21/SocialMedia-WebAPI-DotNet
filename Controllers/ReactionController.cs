using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOCIALIZE.Data;
using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;
using System.Security.Claims;

namespace SOCIALIZE.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReactionController : ControllerBase
    {
        private readonly IReactionService _reactionService;

        public ReactionController(IReactionService reactionService) => _reactionService = reactionService;

        [HttpPost("toggle")]
        public async Task<IActionResult> Toggle(int postId ,ReactType type)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var success = await _reactionService.ToggleReactionAsync(userId, postId, type);
            return success ? Ok() : BadRequest();
        }
    }
}

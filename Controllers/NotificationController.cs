using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;
using SOCIALIZE.Repositories;
using System.Security.Claims;


namespace SOCIALIZE.Controllers
{
    [Authorize]
    [ApiController] 
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService) => _notificationService = notificationService;

        [HttpGet("api/[controller]")]
        public async Task<IActionResult> GetMyNotifications()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User identity not found.");
            }

            var notifications = await _notificationService.GetMyNotificationsAsync(userId);

            return Ok(notifications);
        }

        [HttpPatch("api/[controller]/{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var success = await _notificationService.MarkAsReadAsync(userId, id);

            return success ? Ok() : NotFound("Notification not found or access denied.");
        }
    }
}
       
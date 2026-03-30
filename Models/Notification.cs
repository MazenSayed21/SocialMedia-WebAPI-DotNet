using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOCIALIZE.Models
{
    public class Notification
    {
      [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty; // e.g., "Reaction", "Comment", "FriendRequest"

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // The user who receives the notification
        public string? ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public AppUser? Receiver { get; set; }

        // Optional: Link to the specific post or comment that triggered it
        public string? TargetId { get; set; }
    }
}

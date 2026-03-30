using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOCIALIZE.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public int ?PostId { get; set; }
        [ForeignKey("PostId")]
        public Post? Post { get; set; }

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser? Creator { get; set; }

        // Self-referencing for replies
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Comment? ParentComment { get; set; }

        public DateTime CreatedAt { set; get; }
    }
}

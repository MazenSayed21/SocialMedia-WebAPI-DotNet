using SOCIALIZE.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOCIALIZE.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser? Creator { get; set; }
        public DateTime? CreatedAt { set; get; }
    }
}

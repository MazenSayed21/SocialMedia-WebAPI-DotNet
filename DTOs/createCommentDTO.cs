using System.ComponentModel.DataAnnotations;

namespace SOCIALIZE.DTOs
{
    public class createCommentDTO
    {
        [Required]
        public string Content { get; set; }

        
        public int? PostId { get; set; }

        public int? parentId { set; get; }

    }
}

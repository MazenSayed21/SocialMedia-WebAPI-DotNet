namespace SOCIALIZE.DTOs
{
    public class readCommentDTO
    {
        public int commentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public int? PostId { get; set; }

        public string UserId { get; set; }
        public string AuthorName { get; set; }
    }
}

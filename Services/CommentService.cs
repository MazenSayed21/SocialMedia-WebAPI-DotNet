using SOCIALIZE.DTOs;
using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;

namespace SOCIALIZE.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepo _commentRepo;
        private readonly INotificationRepo _notificationRepo;
        private readonly IPostRepo _postRepo;

        public CommentService(ICommentRepo commentRepo, INotificationRepo notificationRepo, IPostRepo postRepo)
        {
            _commentRepo = commentRepo;
            _notificationRepo = notificationRepo;
            _postRepo = postRepo;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _commentRepo.GetCommentsByPostIdAsync(postId);
        }

        public async Task<Comment?> AddCommentAsync(string userId, int? postId, string content, int? parentId = null)
        {
            if (postId == null) return null;
            else
            {
                var post = await _postRepo.GetPostByIdAsync((int)postId);
                if (post == null) return null;

                var comment = new Comment
                {
                    UserId = userId,
                    PostId = postId,
                    Content = content,
                    ParentId = parentId,
                    CreatedAt = DateTime.UtcNow
                };

                var success = await _commentRepo.CreateCommentAsync(comment);

                if (success)
                {
                    // Business Logic: Notify the Post Owner (if it's not their own post)
                    if (post.UserId != userId)
                    {
                        await _notificationRepo.CreateNotificationAsync(new Notification
                        {
                            ReceiverId = post.UserId,
                            Message = $"Someone commented on your post: \"{content.Substring(0, Math.Min(20, content.Length))}...\"",
                            Type = "Comment",
                            TargetId = postId.ToString(),
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }

                return comment;

            }

        }

        public async Task<bool> UpdateCommentAsync(string userId, updateCommentDTO dto)
        {
            var comment = await _commentRepo.GetCommentByIdAsync(dto.id);

            // Ownership Check: Only the person who wrote the comment can edit it
            if (comment == null || comment.UserId != userId) return false;

            comment.Content = dto.content;
            return await _commentRepo.UpdateCommentAsync(comment);
        }

        public async Task<bool> DeleteCommentAsync(string userId, string userRole, int commentId)
        {
            var comment = await _commentRepo.GetCommentByIdAsync(commentId);
            if (comment == null) return false;

            // Business Logic: Who can delete a comment?
            // 1. The owner of the comment
            // 2. The owner of the post where the comment is located
            // 3. An Admin
            bool isCommentOwner = comment.UserId == userId;
            bool isPostOwner = comment.Post?.UserId == userId;
            bool isAdmin = userRole == "Admin";

            if (isCommentOwner || isPostOwner || isAdmin)
            {
                return await _commentRepo.DeleteCommentAsync(commentId);
            }

            return false;
        }
    }
}

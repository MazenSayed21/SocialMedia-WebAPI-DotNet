using SOCIALIZE.Data;
using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;

namespace SOCIALIZE.Services
{
    public class ReactionService:IReactionService
    {
        private readonly IReactionRepo _reactionRepo;
        private readonly IPostRepo _postRepo;
        private readonly INotificationRepo _notificationRepo;

        public ReactionService(IReactionRepo reactionRepo, IPostRepo postRepo, INotificationRepo notificationRepo)
        {
            _reactionRepo = reactionRepo;
            _postRepo = postRepo;
            _notificationRepo = notificationRepo;
        }

        public async Task<bool> ToggleReactionAsync(string userId, int? postId, ReactType type)
        {
            var post = await _postRepo.GetPostByIdAsync((int)postId);
            if (post == null) return false;

            // Check if the user has already reacted to this post
            var existingReaction = await _reactionRepo.GetReactionByUserAndPostAsync(userId, (int)postId);

            if (existingReaction != null)
            {
                // If reaction exists, we "Unlike" (Remove it)
                return await _reactionRepo.DeleteReactionAsync(existingReaction.Id);
            }
            else
            {
                // If reaction doesn't exist, we "Like" (Add it)
                var reaction = new Reaction
                {
                    UserId = userId,
                    PostId = postId,
                    Type = type
                };

                var success = await _reactionRepo.AddReactionAsync(reaction);

                if (success)
                {
                    // Business Logic: Notify the Post Owner
                    if (post.UserId != userId)
                    {
                        await _notificationRepo.CreateNotificationAsync(new Notification
                        {
                            ReceiverId = post.UserId,
                            Message = $"Someone reacted to your post with {type}!",
                            Type = "Reaction",
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }
                return success;
            }
        }

        public async Task<IEnumerable<Reaction>> GetReactionsForPostAsync(int postId)
        {
            return await _reactionRepo.GetReactionsByPostIdAsync(postId);
        }

        public async Task<int> GetReactionCountAsync(int postId)
        {
            return await _reactionRepo.GetReactionCountByPostIdAsync(postId);
        }

    }
}

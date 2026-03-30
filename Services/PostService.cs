using SOCIALIZE.Models;
using SOCIALIZE.Interfaces;
using SOCIALIZE.DTOs;

namespace SOCIALIZE.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepo _postRepo;
        private readonly IFriendshipService _friendshipService;

        public PostService(IPostRepo postRepo, IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
            _postRepo = postRepo;
        }
        public async Task<IEnumerable<Post>> GetFeedAsync(string userId)
        {
            var friendIds = await _friendshipService.GetFriendsListAsync(userId);

            var feed = await _postRepo.GetPostsByMultipleUserIdsAsync(friendIds);

            return feed.OrderByDescending(p => p.CreatedAt);
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _postRepo.GetPostByIdAsync(id);
        }

        public async Task<Post> CreatePostAsync(CreatePostDTO dto, string? mediaUrl)
        {
            var post = new Post
            {
                UserId = dto.cereatoId,
                Content = dto.content,
                CreatedAt = DateTime.UtcNow
            };

            if (await _postRepo.CreatePostAsync(post)) return post;
            return null;
        }

        public async Task<bool> UpdatePostAsync(string userId, updatePostDTO dto)
        {
            var post = await _postRepo.GetPostByIdAsync(dto.postId);

            // Ownership Check: Only the creator can edit their own post
            if (post == null || post.UserId != userId)
            {
                return false;
            }

            post.Content = dto.content;
            return await _postRepo.UpdatePostAsync(post);
        }

        public async Task<bool> DeletePostAsync(string userId, string userRole, int postId)
        {
            var post = await _postRepo.GetPostByIdAsync(postId);
            if (post == null) return false;

            // Authorization Logic: 
            // A post can be deleted if the user is the Owner OR the user is an Admin
            if (post.UserId == userId || userRole == "Admin")
            {
                return await _postRepo.DeletePostAsync(postId);
            }

            return false;
        }
    }
}

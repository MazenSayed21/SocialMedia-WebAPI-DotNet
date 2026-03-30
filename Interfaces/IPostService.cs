using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SOCIALIZE.DTOs;
using SOCIALIZE.Models;

namespace SOCIALIZE.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetFeedAsync(string userId);
        Task<Post?> GetPostByIdAsync(int id);
        Task<Post> CreatePostAsync(CreatePostDTO dto, string? mediaUrl);
        Task<bool> UpdatePostAsync(string userId,updatePostDTO dto);
        Task<bool> DeletePostAsync(string userId, string userRole, int postId);
    }
}

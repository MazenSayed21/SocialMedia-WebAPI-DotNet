using Microsoft.EntityFrameworkCore;
using SOCIALIZE.Models;

namespace SOCIALIZE.Interfaces
{
    public interface IPostRepo
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post?> GetPostByIdAsync(int id);
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(string userId);
        Task<bool> CreatePostAsync(Post post);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(int id);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Post>> GetPostsByMultipleUserIdsAsync(IEnumerable<string> userIds);
        
    }
}

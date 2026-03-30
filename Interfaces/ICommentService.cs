using SOCIALIZE.DTOs;
using SOCIALIZE.Models;

namespace SOCIALIZE.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
        Task<Comment?> AddCommentAsync(string userId, int? postId, string content, int? parentId = null);
        Task<bool> UpdateCommentAsync(string userId, updateCommentDTO dto);
        Task<bool> DeleteCommentAsync(string userId, string userRole, int commentId);
    }
}

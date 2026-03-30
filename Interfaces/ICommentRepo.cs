using SOCIALIZE.Models;

namespace SOCIALIZE.Interfaces
{
    public interface ICommentRepo
    {
        Task<Comment?> GetCommentByIdAsync(int id);
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
        Task<bool> CreateCommentAsync(Comment comment);
        Task<bool> UpdateCommentAsync(Comment comment);
        Task<bool> DeleteCommentAsync(int id);
        Task<bool> DeleteRepliesByParentIdAsync(int parentId);
        Task<bool> SaveChangesAsync();
    }
}

using SOCIALIZE.Models;

namespace SOCIALIZE.Interfaces
{
    public interface IReactionRepo
    {
        Task<Reaction?> GetReactionByUserAndPostAsync(string userId, int postId);
        Task<IEnumerable<Reaction>> GetReactionsByPostIdAsync(int postId);
        Task<bool> AddReactionAsync(Reaction reaction);
        Task<bool> DeleteReactionAsync(int id);
        Task<int> GetReactionCountByPostIdAsync(int postId);
        Task<bool> SaveChangesAsync();
    }
}

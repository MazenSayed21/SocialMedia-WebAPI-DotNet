using SOCIALIZE.Data;
using SOCIALIZE.Models;

namespace SOCIALIZE.Interfaces
{
    public interface IReactionService
    {
        Task<bool> ToggleReactionAsync(string userId, int ?postId, ReactType type);
        Task<IEnumerable<Reaction>> GetReactionsForPostAsync(int postId);
        Task<int> GetReactionCountAsync(int postId);
    }
}

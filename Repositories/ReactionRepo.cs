using Microsoft.EntityFrameworkCore;
using SOCIALIZE.Data;
using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;

namespace SOCIALIZE.Repositories
{
    public class ReactionRepo : IReactionRepo
    {
        private readonly AppDbContext _context;

        public ReactionRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reaction?> GetReactionByUserAndPostAsync(string userId, int postId)
        {
            // Used to check if the user has already reacted to this post
            return await _context.reactions
                .FirstOrDefaultAsync(r => r.UserId == userId && r.PostId == postId);
        }

        public async Task<IEnumerable<Reaction>> GetReactionsByPostIdAsync(int postId)
        {
            return await _context.reactions
                .Include(r => r.User)
                .Where(r => r.PostId == postId)
                .ToListAsync();
        }

        public async Task<int> GetReactionCountByPostIdAsync(int postId)
        {
            return await _context.reactions
                .CountAsync(r => r.PostId == postId);
        }

        public async Task<bool> AddReactionAsync(Reaction reaction)
        {
            await _context.reactions.AddAsync(reaction);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteReactionAsync(int id)
        {
            var reaction = await _context.reactions.FindAsync(id);
            if (reaction == null) return false;

            _context.reactions.Remove(reaction);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}

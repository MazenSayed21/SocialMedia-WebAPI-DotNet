using Microsoft.EntityFrameworkCore;
using SOCIALIZE.Data;
using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;

namespace SOCIALIZE.Repositories
{
    public class CommentRepo : ICommentRepo
    {
        private readonly AppDbContext _context;

        public CommentRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            // We include Creator for the username and Post to check for Post Owner permissions
            return await _context.comments
                .Include(c => c.Creator)
                .Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            // Fetches only top-level comments (where ParentId is null) 
            // and includes their first level of replies
            return await _context.comments
                .Include(c => c.Creator)
                .Where(c => c.PostId == postId && c.ParentId == null)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> CreateCommentAsync(Comment comment)
        {
            await _context.comments.AddAsync(comment);
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateCommentAsync(Comment comment)
        {
            _context.comments.Update(comment);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            var comment = await _context.comments.FindAsync(id);
            if (comment == null) return false;

            _context.comments.Remove(comment);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteRepliesByParentIdAsync(int parentId)
        {
            // Select all comments where the parent matches
            var replies = await _context.comments
                .Where(c => c.ParentId == parentId)
                .ToListAsync();

            if (replies.Any())
            {
                _context.comments.RemoveRange(replies);
                return await SaveChangesAsync();
            }
            return true; // Return true even if no replies were found to delete
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

    }
}

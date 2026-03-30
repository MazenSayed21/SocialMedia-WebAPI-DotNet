using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOCIALIZE.Data;
using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;

namespace SOCIALIZE.Repositories
{
    public class PostRepo : IPostRepo
    {
        private readonly AppDbContext _context;

        public PostRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            // We include the Creator to show the username on the feed
            return await _context.posts
                .Include(p => p.Creator)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _context.posts
                .Include(p => p.Creator)
                .Include(p => p.CreatedAt)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(string userId)
        {
            return await _context.posts
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            await _context.posts.AddAsync(post);
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            _context.posts.Update(post);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var post = await _context.posts.FindAsync(id);
            if (post == null) return false;

            _context.posts.Remove(post);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Returns true if one or more rows were affected in the DB
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<IEnumerable<Post>> GetPostsByMultipleUserIdsAsync(IEnumerable<string> userIds)
        {
            return await _context.posts
                .Where(p => userIds.Contains(p.UserId)) // SQL: WHERE AuthorId IN ('id1', 'id2'...)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

    }
}

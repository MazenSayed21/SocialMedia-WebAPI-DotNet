using Microsoft.EntityFrameworkCore;
using SOCIALIZE.Data;
using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;


namespace SOCIALIZE.Repositories
{
    public class FriendShipRepo:IFriendShipRepo
    {
        private readonly AppDbContext _context;

        public FriendShipRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FriendShip?> GetByIdAsync(int id)
        {
            return await _context.friendShips.FindAsync(id);
        }

        public async Task<FriendShip?> GetFriendshipAsync(string user1Id, string user2Id)
        {
            // Checks for friendship regardless of who was the requestor or receiver
            return await _context.friendShips
                .FirstOrDefaultAsync(f =>
                    (f.RequestorId == user1Id && f.ReceiverId == user2Id) ||
                    (f.RequestorId == user2Id && f.ReceiverId == user1Id));
        }

        public async Task<IEnumerable<FriendShip>> GetPendingRequestsByUserIdAsync(string userId)
        {
            return await _context.friendShips
                .Include(f => f.Requestor)
                .Where(f => f.ReceiverId == userId && f.Status == "Pending")
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAcceptedFriendsAsync(string userId)
        {
            return await _context.friendShips
                .Where(f => (f.ReceiverId == userId || f.RequestorId == userId) && f.Status == "Accepted")
                .Select(f => f.ReceiverId == userId ? f.RequestorId : f.ReceiverId)
                .ToListAsync();
        }
        public async Task<bool> CreateFriendshipAsync(FriendShip friendship)
        {
            await _context.friendShips.AddAsync(friendship);
            return await SaveChangesAsync();
        }

        public async Task<bool> UpdateFriendshipAsync(FriendShip friendship)
        {
            _context.friendShips.Update(friendship);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteFriendshipAsync(int id)
        {
            var friendship = await _context.friendShips.FindAsync(id);
            if (friendship == null) return false;

            _context.friendShips.Remove(friendship);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}

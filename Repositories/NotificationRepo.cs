using Microsoft.EntityFrameworkCore;
using SOCIALIZE.Data;
using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;

namespace SOCIALIZE.Repositories
{

    public class NotificationRepo : INotificationRepo
    {
        private readonly AppDbContext _context;

        public NotificationRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Notification?> GetNotificationByIdAsync(int id)
        {
            return await _context.notifications.FindAsync(id);
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId)
        {
            return await _context.notifications
                .Where(n => n.ReceiverId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> GetUnreadCountAsync(string userId)
        {
            return await _context.notifications
                .CountAsync(n => n.ReceiverId == userId && !n.IsRead);
        }

        public async Task<bool> CreateNotificationAsync(Notification notification)
        {
            await _context.notifications.AddAsync(notification);
            return await SaveChangesAsync();
        }

        public async Task<bool> MarkAsReadAsync(int notificationId)
        {
            var notification = await _context.notifications.FindAsync(notificationId);
            if (notification == null) return false;

            notification.IsRead = true;
            return await SaveChangesAsync();
        }
        public async Task<bool> DeleteNotificationAsync(int id)
        {
            var notification = await _context.notifications.FindAsync(id);
            if (notification == null) return false;

            _context.notifications.Remove(notification);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        Task<bool> INotificationRepo.MarkAllAsReadAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

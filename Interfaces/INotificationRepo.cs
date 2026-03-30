using SOCIALIZE.Models;

namespace SOCIALIZE.Interfaces
{
    public interface INotificationRepo
    {
        Task<Notification?> GetNotificationByIdAsync(int id);
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId);
        Task<int> GetUnreadCountAsync(string userId);
        Task<bool> CreateNotificationAsync(Notification notification);
        Task<bool> MarkAsReadAsync(int notificationId);
        Task<bool> MarkAllAsReadAsync(string userId);
        Task<bool> DeleteNotificationAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}

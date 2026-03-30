using SOCIALIZE.Models;

namespace SOCIALIZE.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetMyNotificationsAsync(string userId);
        Task<int> GetUnreadCountAsync(string userId);
        Task<bool> MarkAsReadAsync(string userId, int notificationId);
        Task<bool> MarkAllAsReadAsync(string userId);
        Task<bool> DeleteNotificationAsync(string userId, int notificationId);
    }

}

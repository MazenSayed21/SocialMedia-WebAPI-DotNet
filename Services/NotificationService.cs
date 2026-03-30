using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;

namespace SOCIALIZE.Services
{
    public class NotificationService:INotificationService
    {
        private readonly INotificationRepo _notificationRepo;

        public NotificationService(INotificationRepo notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }

        public async Task<IEnumerable<Notification>> GetMyNotificationsAsync(string userId)
        {
            return await _notificationRepo.GetUserNotificationsAsync(userId);
        }

        public async Task<int> GetUnreadCountAsync(string userId)
        {
            return await _notificationRepo.GetUnreadCountAsync(userId);
        }

        public async Task<bool> MarkAsReadAsync(string userId, int notificationId)
        {
            var notification = await _notificationRepo.GetNotificationByIdAsync(notificationId);

            // Security Check: Ensure the notification belongs to the user trying to mark it
            if (notification == null || notification.ReceiverId != userId)
            {
                return false;
            }

            return await _notificationRepo.MarkAsReadAsync(notificationId);
        }

        public async Task<bool> MarkAllAsReadAsync(string userId)
        {
            return await _notificationRepo.MarkAllAsReadAsync(userId);
        }

        public async Task<bool> DeleteNotificationAsync(string userId, int notificationId)
        {
            var notification = await _notificationRepo.GetNotificationByIdAsync(notificationId);

            // Security Check: Ensure the user owns the notification they are deleting
            if (notification == null || notification.ReceiverId != userId)
            {
                return false;
            }

            return await _notificationRepo.DeleteNotificationAsync(notificationId);
        }
    }
}

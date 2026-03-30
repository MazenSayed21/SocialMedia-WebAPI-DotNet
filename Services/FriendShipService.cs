using SOCIALIZE.Interfaces;
using SOCIALIZE.Models;
using SOCIALIZE.Repositories;

namespace SOCIALIZE.Services
{
    public class FriendShipService:IFriendshipService
    {
        private readonly IFriendShipRepo _friendshipRepo;
        private readonly INotificationRepo _notificationRepo;

        public FriendShipService(IFriendShipRepo friendshipRepo, INotificationRepo notificationRepo)
        {
            _friendshipRepo = friendshipRepo;
            _notificationRepo = notificationRepo;
        }

        public async Task<bool> SendFriendRequestAsync(string senderId, string receiverId)
        {
            if (senderId == receiverId) return false;

            // Check if a request already exists in any state (Pending, Accepted, etc.)
            var existing = await _friendshipRepo.GetFriendshipAsync(senderId, receiverId);
            if (existing != null) return false;

            var friendship = new FriendShip
            {
                RequestorId = senderId,
                ReceiverId = receiverId,
                Status = "Pending"
            };

            var success = await _friendshipRepo.CreateFriendshipAsync(friendship);

            if (success)
            {
                await _notificationRepo.CreateNotificationAsync(new Notification
                {
                    ReceiverId = receiverId,
                    Message = "You have a new friend request!",
                    Type = "FriendRequest",
                    TargetId = senderId,
                    CreatedAt = DateTime.UtcNow
                });
            }

            return success;
        }

        public async Task<bool> RespondToRequestAsync(string userId, int requestId, string action)
        {
            var request = await _friendshipRepo.GetByIdAsync(requestId);

            // Security: Ensure the user responding is the actual receiver
            if (request == null || request.ReceiverId != userId || request.Status != "Pending")
                return false;

            if (action.ToLower() == "accept")
            {
                request.Status = "Accepted";
                var success = await _friendshipRepo.UpdateFriendshipAsync(request);

                if (success)
                {
                    await _notificationRepo.CreateNotificationAsync(new Notification
                    {
                        ReceiverId = request.RequestorId,
                        Message = "Your friend request was accepted!",
                        Type = "FriendAccepted",
                        TargetId = userId,
                        CreatedAt = DateTime.UtcNow
                    });
                }
                return success;
            }
            else if (action.ToLower() == "reject")
            {
                request.Status = "Rejected";
                return await _friendshipRepo.UpdateFriendshipAsync(request);
            }

            return false;
        }

        public async Task<IEnumerable<FriendShip>> GetPendingRequestsAsync(string userId)
        {
            return await _friendshipRepo.GetPendingRequestsByUserIdAsync(userId);
        }

        public async Task<IEnumerable<string>> GetFriendsListAsync(string userId)
        {
            return await _friendshipRepo.GetAcceptedFriendsAsync(userId);
        }

        public async Task<bool> RemoveFriendAsync(string userId, string friendId)
        {
            var friendship = await _friendshipRepo.GetFriendshipAsync(userId, friendId);
            if (friendship == null) return false;

            return await _friendshipRepo.DeleteFriendshipAsync(friendship.Id);
        }

        public async Task<string> GetFriendshipStatusAsync(string userId, string otherUserId)
        {
            var friendship = await _friendshipRepo.GetFriendshipAsync(userId, otherUserId);
            return friendship?.Status ?? "None";
        }
    }
}

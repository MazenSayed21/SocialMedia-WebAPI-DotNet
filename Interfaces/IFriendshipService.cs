using SOCIALIZE.Models;

namespace SOCIALIZE.Interfaces
{
    public interface IFriendshipService
    {
        Task<bool> SendFriendRequestAsync(string senderId, string receiverId);
        Task<bool> RespondToRequestAsync(string userId, int requestId, string action); 
        Task<IEnumerable<FriendShip>> GetPendingRequestsAsync(string userId);
        Task<IEnumerable<string>> GetFriendsListAsync(string userId);
        Task<bool> RemoveFriendAsync(string userId, string friendId);
        Task<string> GetFriendshipStatusAsync(string userId, string otherUserId);
    }
}

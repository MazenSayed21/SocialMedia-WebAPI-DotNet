using SOCIALIZE.Models;

namespace SOCIALIZE.Interfaces
{
    public interface IFriendShipRepo
    {
        Task<FriendShip?> GetByIdAsync(int id);
        Task<FriendShip?> GetFriendshipAsync(string user1Id, string user2Id);
        Task<IEnumerable<FriendShip>> GetPendingRequestsByUserIdAsync(string userId);
        Task<IEnumerable<string>> GetAcceptedFriendsAsync(string userId);
        Task<bool> CreateFriendshipAsync(FriendShip friendship);
        Task<bool> UpdateFriendshipAsync(FriendShip friendship);
        Task<bool> DeleteFriendshipAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}

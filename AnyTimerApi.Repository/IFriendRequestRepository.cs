using System.Collections.Generic;
using System.Threading.Tasks;
using AnyTimerApi.Database.Entities;

namespace AnyTimerApi.Repository
{
    public interface IFriendRequestRepository
    {
        Task<FriendRequest> ById(string id);

        Task<FriendRequest> ByUsers(string userId1, string userId2);

        Task<ICollection<FriendRequest>> ReceivedRequests(string userId);

        Task<ICollection<FriendRequest>> SentRequests(string userId);

        Task<ICollection<FriendRequest>> ByUser(string userId);

        Task<ICollection<User>> GetFriends(string userId);
    }
}
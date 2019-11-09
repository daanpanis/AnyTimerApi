using System;
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

        Task<ICollection<FriendRequest>> All(string userId);

        Task<ICollection<FriendRequest>> Requests(string userId);
        
        Task<ICollection<FriendRequest>> GetFriends(string userId);

        Task<FriendRequest> AddFriendRequest(string requesterId, string requestedId, DateTime time);
    }
}
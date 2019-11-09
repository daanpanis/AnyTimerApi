using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnyTimerApi.Database;
using AnyTimerApi.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnyTimerApi.Repository.Database
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly DatabaseContext _context;

        public FriendRequestRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<FriendRequest> ById(string id)
        {
            return await _context.FriendRequests.FirstOrDefaultAsync(request => request.Id.Equals(id));
        }

        public async Task<FriendRequest> ByUsers(string userId1, string userId2)
        {
            return await _context.FriendRequests
                .Where(request => request.RequestedId.Equals(userId1) || request.RequestedId.Equals(userId2))
                .Where(request => request.RequesterId.Equals(userId1) || request.RequesterId.Equals(userId2))
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<FriendRequest>> ReceivedRequests(string userId)
        {
            return await _context.FriendRequests.Where(request => request.RequestedId.Equals(userId)).ToListAsync();
        }

        public async Task<ICollection<FriendRequest>> SentRequests(string userId)
        {
            return await _context.FriendRequests.Where(request => request.RequesterId.Equals(userId)).ToListAsync();
        }

        public async Task<ICollection<FriendRequest>> All(string userId)
        {
            return await _context.FriendRequests
                .Where(request => request.RequesterId.Equals(userId) || request.RequestedId.Equals(userId))
                .ToListAsync();
        }

        public async Task<ICollection<FriendRequest>> Requests(string userId)
        {
            return await _context.FriendRequests
                .Where(request => request.RequesterId.Equals(userId) || request.RequestedId.Equals(userId))
                .Where(request => request.Status == FriendRequestStatus.Requested)
                .ToListAsync();
        }

        public async Task<ICollection<FriendRequest>> GetFriends(string userId)
        {
            return await _context.FriendRequests
                .Where(request => (request.RequesterId.Equals(userId) || request.RequestedId.Equals(userId)) &&
                                  request.Status == FriendRequestStatus.Accepted)
                .ToListAsync();
        }

        public async Task<FriendRequest> AddFriendRequest(string requesterId, string requestedId, DateTime time)
        {
            var request = new FriendRequest
            {
                Id = Guid.NewGuid().ToString(),
                RequesterId = requestedId,
                RequestedId = requestedId,
                Status = FriendRequestStatus.Requested,
                CreatedTime = time
            };
            await _context.FriendRequests.AddAsync(request);
            return request;
        }
    }
}
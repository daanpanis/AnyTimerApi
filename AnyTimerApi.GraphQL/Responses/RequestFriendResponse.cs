using System;
using AnyTimerApi.Database.Entities;

namespace AnyTimerApi.GraphQL.Responses
{
    public class RequestFriendResponse
    {
        public string RequesterId { get; set; }
        public User Requester { get; set; }

        public string RequestedId { get; set; }
        public User Requested { get; set; }

        public DateTime Time { get; set; }
    }
}
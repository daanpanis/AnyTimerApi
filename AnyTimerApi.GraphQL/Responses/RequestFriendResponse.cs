using System;

namespace AnyTimerApi.GraphQL.Responses
{
    public class RequestFriendResponse
    {
        public string RequesterId { get; set; }
        public string RequestedId { get; set; }
        public DateTime Time { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnyTimerApi.Database.Entities
{
    public class FriendRequest
    {
        [Key] public string Id { get; set; }
        [Required] public FriendRequestStatus Status { get; set; }
        public string RequesterId { get; set; }
        [ForeignKey("RequesterId")] public User Requester { get; set; }

        public string RequestedId { get; set; }
        [ForeignKey("RequestedId")] public User Requested { get; set; }
    }
}
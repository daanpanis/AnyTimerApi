using System;
using System.ComponentModel.DataAnnotations;

namespace AnyTimerApi.Database.Entities
{
    public class FriendRequest
    {
        [Key] public string Id { get; set; }
        [Required] public FriendRequestStatus Status { get; set; }
        [Required] public string RequesterId { get; set; }

        [Required] public string RequestedId { get; set; }

        [Required] public DateTime CreatedTime { get; set; }
    }
}
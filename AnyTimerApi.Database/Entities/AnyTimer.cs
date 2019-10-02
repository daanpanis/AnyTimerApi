using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnyTimerApi.Database.Entities
{
    public class AnyTimer
    {
        [Key] public Guid Id { get; set; }

        [Required] public uint Amount { get; set; }

        public string RequesterId { get; set; }
        [ForeignKey("RequesterId")] public User Requester { get; set; }

        public string ReceiverId { get; set; }
        [ForeignKey("ReceiverId")] public User Receiver { get; set; }
    }
}
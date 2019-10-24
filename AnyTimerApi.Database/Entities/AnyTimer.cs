using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnyTimerApi.Database.Entities
{
    public class AnyTimer
    {
        [Key] public string Id { get; set; }
        [Required] public DateTime CreatedTime { get; set; }
        [Required] public DateTime LastUpdated { get; set; }
        [Required] public AnyTimerStatus Status { get; set; }
        [Required] public string ReceiverId { get; set; }
        [ForeignKey("ReceiverId")] public User Receiver { get; set; }
        public virtual ICollection<User> Senders { get; set; }
        public virtual ICollection<StatusEvent> StatusEvents { get; set; }
    }
}
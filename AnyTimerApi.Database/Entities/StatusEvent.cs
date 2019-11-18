using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnyTimerApi.Database.Entities
{
    public class StatusEvent
    {
        [Key] public string AnyTimerId { get; set; }
        [ForeignKey("AnyTimerId")] public AnyTimer AnyTimer { get; set; }
        [Key] public AnyTimerStatus Status { get; set; }
        [Required] public DateTime EventTime { get; set; }
        public string Message { get; set; }
    }
}
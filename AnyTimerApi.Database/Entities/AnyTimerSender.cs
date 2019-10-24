using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnyTimerApi.Database.Entities
{
    public class AnyTimerSender
    {
        [Key] public string AnyTimerId { get; set; }
        [ForeignKey("AnyTimerId")] public AnyTimer AnyTimer { get; set; }
        [Key] public string SenderId { get; set; }
        [ForeignKey("SenderId")] public User Sender { get; set; }
        [Required] public uint Amount { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnyTimerApi.Database.Entities
{
    public class Comment
    {
        [Key] public string AnyTimerId { get; set; }
        [ForeignKey("AnyTimerId")] public AnyTimer AnyTimer { get; set; }
        [Key] public string UserId { get; set; }
        [Key] public DateTime Time { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(512)]
        public string Text { get; set; }
    }
}
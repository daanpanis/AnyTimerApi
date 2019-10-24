using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnyTimerApi.Database.Entities
{
    public class User
    {
        [Key] public string Id { get; set; }

        [Required, MinLength(2), MaxLength(100)]
        public string Name { get; set; }

        [Required] public int Age { get; set; }
    }
}
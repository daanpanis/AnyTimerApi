using AnyTimerApi.Database.Entities;
using Marques.EFCore.SnakeCase;
using Microsoft.EntityFrameworkCore;

namespace AnyTimerApi.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ToSnakeCase();


            builder.Entity<Comment>().HasKey(comment => new
            {
                comment.AnyTimerId, comment.UserId, comment.Time
            });
            builder.Entity<Comment>().HasOne(comment => comment.AnyTimer);
            builder.Entity<Comment>().Property(c => c.Edited).HasDefaultValue(false);
            
            builder.Entity<AnyTimerSender>().HasKey(sender => new {sender.AnyTimerId, sender.SenderId});
            builder.Entity<AnyTimerSender>().HasOne(sender => sender.AnyTimer);

            builder.Entity<StatusEvent>().HasKey(evt => new {evt.AnyTimerId, evt.Status});
            builder.Entity<StatusEvent>().HasOne(e => e.AnyTimer);

            builder.Entity<AnyTimer>().HasMany(anyTimer => anyTimer.Senders);
            builder.Entity<AnyTimer>().HasMany(anyTimer => anyTimer.StatusEvents);
        }


        public DbSet<AnyTimer> AnyTimers { get; set; }
        public DbSet<AnyTimerSender> AnyTimerSenders { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<StatusEvent> StatusEvents { get; set; }
    }
}
using AnyTimerApi.Database.Entities;
using AnyTimerApi.Utilities.Extensions;
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

            builder.Entity<StatusEvent>().HasKey(evt => new {evt.AnyTimerId, evt.Status});
            builder.Entity<Comment>().HasKey(comment => new
            {
                comment.AnyTimerId, comment.UserId, comment.Time
            });
            builder.Entity<AnyTimerSender>().HasKey(sender => new {sender.AnyTimerId, sender.SenderId});
            builder.Entity<FriendRequest>().HasOne(request => request.Requester);
            builder.Entity<FriendRequest>().HasOne(request => request.Requested);

            // Use Snake Case naming conventions in database
            builder.Model.GetEntityTypes().ForEach(entity =>
            {
                /*entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();
                entity.GetProperties().ForEach(property =>
                    property.Relational().ColumnName = property.Relational().ColumnName.ToSnakeCase());
                entity.GetKeys().ForEach(key => key.Relational().Name = key.Relational().Name.ToSnakeCase());
                entity.GetForeignKeys().ForEach(key => key.Relational().Name = key.Relational().Name.ToSnakeCase());
                entity.GetIndexes().ForEach(index => index.Relational().Name = index.Relational().Name.ToSnakeCase());*/
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AnyTimer> AnyTimers { get; set; }
        public DbSet<AnyTimerSender> AnyTimerSenders { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<StatusEvent> StatusEvents { get; set; }
    }
}
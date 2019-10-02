using System.Collections.Generic;
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

            // Use Snake Case naming conventions in database
            builder.Model.GetEntityTypes().ForEach(entity =>
            {
                entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();
                entity.GetProperties().ForEach(property =>
                    property.Relational().ColumnName = property.Relational().ColumnName.ToSnakeCase());
                entity.GetKeys().ForEach(key => key.Relational().Name = key.Relational().Name.ToSnakeCase());
                entity.GetForeignKeys().ForEach(key => key.Relational().Name = key.Relational().Name.ToSnakeCase());
                entity.GetIndexes().ForEach(index => index.Relational().Name = index.Relational().Name.ToSnakeCase());
            });
        }

        public DbSet<User> Users { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using Resume.Entities;
using System;
using Resume.Utils.ExtensionMethods;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Resume.DbContext.Configurations;

namespace Resume.DbContext
{
    public class ResumeDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ResumeDbContext(DbContextOptions<ResumeDbContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.ForNpgsqlUseIdentityColumns();
            //builder.Entity<Message>()
            //    .Property(x => x.ClusteredIndex)
            //    .UseNpgsqlIdentityAlwaysColumn();

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                entityType.Relational().TableName = entityType.DisplayName().ToSnakeCase(); // entityType.Relational().TableName.ToSnakeCase();

                foreach (var property in entityType.GetProperties())
                    property.Relational().ColumnName = property.Name.ToSnakeCase();

                foreach (var key in entityType.GetKeys())
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();

                foreach (var key in entityType.GetForeignKeys())
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();

                foreach (var index in entityType.GetIndexes())
                    index.Relational().Name = index.Relational().Name.ToSnakeCase();
            }

            builder.ApplyConfiguration(new MessageConfigurations());
        }
    }
}

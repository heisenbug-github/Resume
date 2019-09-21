using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resume.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.DbContext.Configurations
{
    public class BaseEntityConfigurations<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ClusteredIndex).UseNpgsqlIdentityAlwaysColumn();
            builder.ForNpgsqlHasIndex(x => x.ClusteredIndex).IsUnique();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Resume.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.DbContext.Configurations
{
    public class LogConfigurations : BaseEntityConfigurations<Log>
    {
        public override void Configure(EntityTypeBuilder<Log> builder)
        {
            base.Configure(builder); // Must call this

            // other configurations here
            builder.Property(x => x.EntityName).IsRequired();
            builder.Property(x => x.ChangeType).HasConversion(new EnumToStringConverter<EntityChangeType>());
            builder.ForNpgsqlHasIndex(x => x.VisitId);

            builder.HasMany(x=>x.LogDetails)
                .WithOne(x=>x.Log)
                .HasForeignKey(x=>x.LogId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

            builder.HasOne<User>()
                .WithMany(x => x.Logs)
                .HasForeignKey(x => x.UserId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.SetNull);
        }
    }
}

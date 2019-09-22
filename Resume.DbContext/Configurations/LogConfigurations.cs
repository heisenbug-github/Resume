using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

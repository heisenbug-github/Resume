using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resume.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.DbContext.Configurations
{
    public class LogDetailConfigurations : BaseEntityConfigurations<LogDetail>
    {
        public override void Configure(EntityTypeBuilder<LogDetail> builder)
        {
            base.Configure(builder); // Must call this

            // other configurations here
            builder.Property(x => x.NewValue).IsRequired();
            builder.Property(x => x.OriginalValue).IsRequired();
            builder.Property(x => x.PropertyName).IsRequired();
        }
    }
}

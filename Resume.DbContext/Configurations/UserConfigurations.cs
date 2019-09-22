using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resume.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.DbContext.Configurations
{
    public class UserConfigurations : BaseEntityConfigurations<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder); // Must call this

            // other configurations here
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.UserName).IsRequired();
        }
    }
}

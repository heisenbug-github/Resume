using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resume.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.DbContext.Configurations
{
    public class MessageConfigurations : BaseEntityConfigurations<Message>
    {
        public override void Configure(EntityTypeBuilder<Message> builder)
        {
            base.Configure(builder); // Must call this

            // other configurations here
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public long ClusteredIndex { get; set; }
    }
}

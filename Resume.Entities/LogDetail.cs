using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.Entities
{
    public class LogDetail : BaseEntity
    {
        public Guid LogId { get; set; }
        public Log Log { get; set; }
        public string PropertyName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
    }
}

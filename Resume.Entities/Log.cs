using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.Entities
{
    public class Log : BaseEntity
    {
        public Guid RecordId { get; set; } // no relation
        public Guid? UserId { get; set; }
        public EntityChangeType ChangeType { get; set; }
        public DateTime ChangeDate { get; set; }
        public string EntityName { get; set; }
        public IList<LogDetail> LogDetails { get; set; }

        public Log()
        {
            LogDetails = new List<LogDetail>();
        }
    }
}

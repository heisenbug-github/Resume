using System;

namespace Resume.Entities
{
    public class Message : BaseEntity
    {
        public string Subject { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Body { get; set; }
        public DateTime MessageDate { get; set; }
        public bool IsRead { get; set; }
    }
}

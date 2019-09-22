using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<Log> Logs { get; set; }

        public User()
        {
            Logs = new List<Log>();
        }
    }
}

using Resume.DbContext;
using Resume.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(ResumeDbContext resumeDbContext) : base(resumeDbContext)
        {

        }
    }
}

using Resume.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Message> MessageRepository { get; }
           
        int SaveChanges();
    }
}

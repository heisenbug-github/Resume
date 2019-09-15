using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IMessageRepository MessageRepository { get; }
           
        int SaveChanges();
    }
}

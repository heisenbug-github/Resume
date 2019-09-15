using Resume.DbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.Repositories.UnitOfWork
{
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ResumeDbContext resumeDbContext;

        public UnitOfWork(ResumeDbContext resumeDbContext)
        {
            this.resumeDbContext = resumeDbContext ?? throw new ArgumentNullException("dbContext can not be null.");

            // Buradan istediğiniz gibi EntityFramework'ü konfigure edebilirsiniz.
            //this.appDbContext.Configuration.LazyLoadingEnabled = false;
            //_dbContext.Configuration.ValidateOnSaveEnabled = false;
            //_dbContext.Configuration.ProxyCreationEnabled = false;
        }

        private IMessageRepository messageRepository;
        public IMessageRepository MessageRepository
        {
            get
            {
                return this.messageRepository ?? (this.messageRepository = new MessageRepository(this.resumeDbContext));
            }
        }


        #region IUnitOfWork Members

        public int SaveChanges()
        {
            try
            {
                using (var transaction = this.resumeDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        int retVal = this.resumeDbContext.SaveChanges();
                        transaction.Commit();
                        return retVal;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                //TODO:Logging
                throw;
            }

            /*
            try
            {
                // Transaction işlemleri burada ele alınabilir veya Identity Map kurumsal tasarım kalıbı kullanılarak
                // sadece değişen alanları güncellemeyide sağlayabiliriz.
                return appDbContext.SaveChanges();
            }
            catch
            {
                // Burada DbEntityValidationException hatalarını handle edebiliriz.
                throw;
            }
            */
        }
        #endregion

        #region IDisposable Members
        // Burada IUnitOfWork arayüzüne implemente ettiğimiz IDisposable arayüzünün Dispose Patternini implemente ediyoruz.
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    resumeDbContext.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

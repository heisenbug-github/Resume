﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Resume.DbContext;
using Resume.Entities;
using Resume.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resume.Repositories.UnitOfWork
{
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ResumeDbContext resumeDbContext;
        private readonly Guid visitId;

        public UnitOfWork(ResumeDbContext resumeDbContext, VisitIdProvider visitIdProvider)
        {
            this.resumeDbContext = resumeDbContext ?? throw new ArgumentNullException("dbContext can not be null.");
            this.visitId = visitIdProvider.VisitId;

            // Buradan istediğiniz gibi EntityFramework'ü konfigure edebilirsiniz.
            //this.appDbContext.Configuration.LazyLoadingEnabled = false;
            //_dbContext.Configuration.ValidateOnSaveEnabled = false;
            //_dbContext.Configuration.ProxyCreationEnabled = false;
        }

        private IBaseRepository<Message> messageRepository;
        public IBaseRepository<Message> MessageRepository
        {
            get
            {
                return this.messageRepository ?? (this.messageRepository = new BaseRepository<Message>(this.resumeDbContext));
            }
        }


        #region IUnitOfWork Members

        public int SaveChanges()
        {
            using (var transaction = this.resumeDbContext.Database.BeginTransaction())
            {
                try
                {
                    this.CreateLogData();// if an error occures in log creation, then no data is saved.
                    int retVal = this.resumeDbContext.SaveChanges();
                    transaction.Commit();
                    return retVal;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //TODO:Logging
                    throw;
                }
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

        private EntityChangeType DetectEntityChangeType(EntityEntry entry)
        {
            EntityChangeType entityChangeType;
            var entryState = entry.State;
            if (entryState == Microsoft.EntityFrameworkCore.EntityState.Added)
            {
                entityChangeType = EntityChangeType.Add;
            }
            else if (entryState == Microsoft.EntityFrameworkCore.EntityState.Modified)
            {
                PropertyEntry isDeletedPropertyEntry = entry.Property("IsDeleted");
                if (isDeletedPropertyEntry.IsModified)
                {
                    if (bool.Parse(isDeletedPropertyEntry.CurrentValue.ToString()) == true)
                    {
                        entityChangeType = EntityChangeType.Delete;
                    }
                    else
                    {
                        entityChangeType = EntityChangeType.Undelete;
                    }
                }
                else
                {
                    entityChangeType = EntityChangeType.Update;
                }
            }
            else
            {
                entityChangeType = EntityChangeType.Drop;
            }

            return entityChangeType;
        }

        private Guid AddLog(EntityEntry entry, EntityChangeType entityChangeType)
        {
            var entityName = entry.Entity.GetType().Name;
            Guid recordId = Guid.Parse(entry.Property("Id").CurrentValue.ToString());

            Log log = new Log
            {
                ChangeDate = DateTime.Now,
                ChangeType = entityChangeType,
                EntityName = entityName,
                RecordId = recordId,
                UserId = null,
                VisitId = this.visitId
            };
            this.resumeDbContext.Logs.Add(log);

            return log.Id;
        }

        private void AddLogDetail(EntityEntry entry, Guid logId)
        {
            foreach (var propertyEntry in entry.Properties)
            {
                if (propertyEntry.Metadata.Name == "ClusteredIndex") continue;

                LogDetail logDetail = new LogDetail
                {
                    LogId = logId,
                    NewValue = propertyEntry.CurrentValue.ToString(),
                    OriginalValue = propertyEntry.OriginalValue.ToString(),
                    PropertyName = propertyEntry.Metadata.Name
                };
                this.resumeDbContext.LogDetails.Add(logDetail);
            }
        }

        private void CreateLogData()
        {
            IEnumerable<EntityEntry> entries = this.resumeDbContext.ChangeTracker.Entries();
            IList<EntityEntry> entityEntries = new List<EntityEntry>(entries);
            foreach (var entry in entityEntries)
            {
                var entityName= entry.Entity.GetType().Name;
                if (entityName == "Log" || entityName == "LogDetail") continue;

                var entityChangeType = this.DetectEntityChangeType(entry);
                var logId = this.AddLog(entry, entityChangeType);
                this.AddLogDetail(entry, logId);
            }
        }

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

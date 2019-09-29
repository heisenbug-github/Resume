using Microsoft.EntityFrameworkCore;
using Resume.DbContext;
using Resume.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
       where TEntity : BaseEntity
    {
        protected readonly Microsoft.EntityFrameworkCore.DbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public BaseRepository(ResumeDbContext resumeDbContext)
        {
            this.dbContext = resumeDbContext ?? throw new ArgumentNullException("dbContext can not be null.");
            this.dbSet = resumeDbContext.Set<TEntity>();
        }

        #region IRepository Members

        public TEntity GetById(Guid? id)
        {
            return this.dbSet.Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> where, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;

            query = query.Where(e => e.IsDeleted == false).Where(where);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.SingleOrDefault();
        }

        public TEntity GetDeleted(Expression<Func<TEntity, bool>> where, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;

            query = query.Where(e => e.IsDeleted == true).Where(where);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.SingleOrDefault();
        }

        public IList<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;
            query = query.Where(e => e.IsDeleted == false);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public IList<TEntity> GetAllDeleted(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;
            query = query.Where(e => e.IsDeleted == true);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public IList<TEntity> GetMany(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;
            query = query.Where(e => e.IsDeleted == false).Where(where);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public IList<TEntity> GetManyDeleted(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;
            query = query.Where(e => e.IsDeleted == true).Where(where);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public void Add(TEntity entity)
        {
            //entity.Id = Guid.NewGuid();
            this.dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {

            if (this.dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }
            //this.dbSet.Attach(entity);
            //this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            //entity.DeletionDate = DateTime.Now;
            Update(entity);



            // Eğer sizlerde genelde bir kayıtı silmek yerine IsDelete şeklinde bool bir flag alanı tutuyorsanız,
            // Küçük bir refleciton kodu yardımı ile bunuda otomatikleştirebiliriz.
            //if (entity.GetType().GetProperty("IsDelete") != null)
            //{
            //    TEntity _entity = entity;

            //    _entity.GetType().GetProperty("IsDelete").SetValue(_entity, true);

            //    this.Update(_entity);
            //}
            //else
            //{
            // Önce entity'nin state'ini kontrol etmeliyiz.
            /*
            DbEntityEntry dbEntityEntry = dbContext.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                this.dbSet.Attach(entity);
                this.dbSet.Remove(entity);
            }
            */
            //    this.dbSet.Attach(entity);
            //    this.dbSet.Remove(entity);
            //}
        }

        public void Delete(Guid id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            else
            {
                Delete(entity);
                /*
                if (entity.GetType().GetProperty("IsDelete") != null)
                {
                    TEntity _entity = entity;
                    _entity.GetType().GetProperty("IsDelete").SetValue(_entity, true);

                    this.Update(_entity);
                }
                else
                {
                    Delete(entity);
                }
                */
            }
        }

        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            var entities = GetMany(where);
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        public void Drop(TEntity entity)
        {
            if (this.dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }
            //this.dbSet.Attach(entity);
            this.dbSet.Remove(entity);
        }

        public void Drop(Guid id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            else
            {
                Drop(entity);
            }
        }

        public void Drop(Expression<Func<TEntity, bool>> where)
        {
            var entities = GetMany(where);
            var deletedEntities = GetManyDeleted(where);
            entities.Concat(deletedEntities);
            foreach (var entity in entities)
            {
                Drop(entity);
            }
        }

        public void UnDelete(TEntity entity)
        {
            entity.IsDeleted = false;
            Update(entity);
        }

        public void UnDelete(Guid id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            else
            {
                UnDelete(entity);
            }
        }

        public void UnDelete(Expression<Func<TEntity, bool>> where)
        {
            var entities = GetManyDeleted(where);
            foreach (var entity in entities)
            {
                UnDelete(entity);
            }
        }

        private async Task<PagedList<TEntity>> GetPagedListAsync(IQueryable<TEntity> source, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 1 : pageSize;
            var totalItemCount = await source.CountAsync();
            var pagedQuery = source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var pagedItemsCount = await pagedQuery.CountAsync();
            var totalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            if (pagedItemsCount == 0)
            {
                pageIndex = totalPageCount == 0 ? 1 : totalPageCount;
                pagedQuery = source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            var pagedItems = await pagedQuery.ToListAsync();
            return new PagedList<TEntity>(pagedItems, totalItemCount, pageIndex, pageSize);
        }

        private PagedList<TEntity> GetPagedList(IList<TEntity> source, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageSize = pageSize < 1 ? 1 : pageSize;
            var totalItemCount = source.Count();
            var pagedItems = source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var pagedItemsCount = pagedItems.Count();
            var totalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            if (pagedItemsCount == 0)
            {
                pageIndex = totalPageCount == 0 ? 1 : totalPageCount;
                pagedItems = source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            return new PagedList<TEntity>(pagedItems.ToList(), totalItemCount, pageIndex, pageSize);
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return this.dbSet.Any(predicate);
        }

        public IList<TEntity> FromSql(string rawSqlString, params object[] parameters)
        {
            return this.dbSet.FromSql<TEntity>(rawSqlString, parameters).ToList();
        }

        /*
        public IQueryable<TEntity> FromSqlQueryable(RawSqlString rawSqlString, params object[] parameters)
        {
            return this.dbSet.FromSql<TEntity>(rawSqlString, parameters);
        }
        */

        public async Task<PagedList<TEntity>> FromSqlAsync(string rawSqlString, int pageIndex, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, params object[] parameters)
        {
            var source = this.dbSet.FromSql<TEntity>(rawSqlString, parameters);
            if (orderBy != null)
            {
                source = orderBy(source);
            }
            var pagedList = await this.GetPagedListAsync(source, pageIndex, pageSize);
            return pagedList;
        }

        public async Task<PagedList<TEntity>> GetAllAsync(int pageIndex, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;
            query = query.Where(e => e.IsDeleted == false);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            var pagedList = await this.GetPagedListAsync(query, pageIndex, pageSize);
            return pagedList;
        }

        public async Task<IList<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;
            query = query.Where(e => e.IsDeleted == false).Where(where);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }

        public async Task<PagedList<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, int pageIndex, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;
            query = query.Where(e => e.IsDeleted == false).Where<TEntity>(where);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await this.GetPagedListAsync(query, pageIndex, pageSize);
        }

        public async Task<IList<TEntity>> GetManyDeletedAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;
            query = query.Where(e => e.IsDeleted == true).Where<TEntity>(where);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.ToListAsync();
        }

        public async Task<PagedList<TEntity>> GetManyDeletedAsync(Expression<Func<TEntity, bool>> where, int pageIndex, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;
            query = query.Where(e => e.IsDeleted == true).Where<TEntity>(where);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await this.GetPagedListAsync(query, pageIndex, pageSize);
        }

        public async Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;
            query = query.Where(e => e.IsDeleted == false);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.ToListAsync();
        }

        public async Task<IList<TEntity>> GetAllDeletedAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;
            query = query.Where(e => e.IsDeleted == true);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.ToListAsync();
        }

        public async Task<PagedList<TEntity>> GetAllDeletedAsync(int pageIndex, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = this.dbSet;
            query = query.Where(e => e.IsDeleted == true);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await this.GetPagedListAsync(query, pageIndex, pageSize);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.dbSet.CountAsync(predicate);
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this.dbSet.Count(predicate);
        }
        #endregion
    }
}

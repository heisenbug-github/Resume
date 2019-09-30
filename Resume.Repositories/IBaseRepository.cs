using Resume.Entities;
using Resume.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Resume.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        // Marks an entity as new
        void Add(TEntity entity);

        // Marks an entity as modified
        void Update(TEntity entity);

        // Marks an entity to be removed
        void Delete(TEntity entity);
        void Delete(Guid id);
        void Delete(Expression<Func<TEntity, bool>> where);

        void Drop(Guid id);
        void Drop(TEntity entity);
        void Drop(Expression<Func<TEntity, bool>> where);

        // Get an entity by int id
        TEntity GetById(Guid? id);
        // Get an entity using delegate
        TEntity Get(Expression<Func<TEntity, bool>> where, string includeProperties = "");
        TEntity GetDeleted(Expression<Func<TEntity, bool>> where, string includeProperties = "");
        // Gets entities using delegate
        IList<TEntity> GetMany(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        IList<TEntity> GetManyDeleted(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        //IQueryable<T> GetMany(Expression<Func<T, bool>> where);
        // Gets all entities of type T
        IList<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        IList<TEntity> GetAllDeleted(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        //IQueryable<T> GetAll();

        void UnDelete(Guid id);
        void UnDelete(TEntity entity);
        void UnDelete(Expression<Func<TEntity, bool>> where);

        bool Exists(Expression<Func<TEntity, bool>> predicate);

        IList<TEntity> FromSql(string rawSqlString, params object[] parameters);
        //IQueryable<TEntity> FromSqlQueryable(RawSqlString rawSqlString, params object[] parameters);
        Task<PagedList<TEntity>> FromSqlAsync(string rawSqlString, int pageIndex, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, params object[] parameters);

        Task<IList<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<PagedList<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, int pageIndex, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<IList<TEntity>> GetManyDeletedAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<PagedList<TEntity>> GetManyDeletedAsync(Expression<Func<TEntity, bool>> where, int pageIndex, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<PagedList<TEntity>> GetAllAsync(int pageIndex, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<IList<TEntity>> GetAllDeletedAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<PagedList<TEntity>> GetAllDeletedAsync(int pageIndex, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        int Count(Expression<Func<TEntity, bool>> predicate);
    }
}

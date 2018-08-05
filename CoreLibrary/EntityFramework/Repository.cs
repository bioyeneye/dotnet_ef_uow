using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CoreLibrary.DataContext;
using CoreLibrary.Repositories;
using CoreLibrary.UnitOfWork;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace CoreLibrary.EntityFramework
{
    public class Repository<T> : IRepositoryAsync<T> where T : class
    {
        protected readonly IDataContextAsync Context;
        private readonly IUnitOfWork _unitOfWork;
        protected readonly DbSet<T> DbSet;


        public Repository(IDataContextAsync context,IUnitOfWork unitOfWork)
        {
            Context = context;
            _unitOfWork = unitOfWork;

            if (context is DbContext dbContext)
            {
                DbSet = dbContext.Set<T>();
            }
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Table;
        }

        public IQueryable<T> Table => Queryable();

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return Table.SingleOrDefault(predicate);
        }

        public void Insert(T entity, bool saveNow = true)
        {
            ((DbContext)Context).Entry(entity).State = EntityState.Added;
            if (saveNow)
                Context.SaveChanges();
        }

        public void InsertRange(IEnumerable<T> entities, bool saveNow = true)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
            if (saveNow)
                Context.SaveChanges();
        }

        public IQueryable<T> Fetch(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? page = null, int? pageSize = null)
        {
            IQueryable<T> query = DbSet;
            if (orderBy != null)
                query = orderBy(query);
            if (predicate != null)
                query = query.AsExpandable().Where(predicate);
            if (page == null || pageSize == null) return query;
            if (page <= 0) page = 1;

            if (pageSize <= 0) pageSize = 10;
            query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);


            return query;
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return Fetch(predicate).Count();
        }

        public void Update(T entity, bool isSaveNow = true)
        {
           
            ((DbContext)Context).Entry(entity).State = EntityState.Modified;
            if (isSaveNow)
            {
                Context.SaveChanges();
            }
        }

        public void Remove(object id, bool saveNow)
        {
            var entity = DbSet.Find(id);
            Remove(entity);
            if (saveNow)
                Context.SaveChanges();
        }

        public virtual void Remove(T entity, bool saveNow = true)
        {
            ((DbContext)Context).Entry(entity).State = EntityState.Deleted;
            if (saveNow)
                Context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities, bool save = true)
        {
            DbSet.RemoveRange(entities);
            if (save)
                SaveChanges();
        }

        public virtual IQueryable<T> Queryable()
        {
            return DbSet;
        }

        //Asynchronous Tasks


       

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }


        public virtual Task<T> FindAsync(params object[] keyValues)
        {
            return FindAsync(CancellationToken.None, keyValues);
        }

        public virtual Task<T> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return DbSet.FindAsync(cancellationToken, keyValues);
        }

        public virtual Task<bool> DeleteAsync(params object[] keyValues)
        {
            return DeleteAsync(CancellationToken.None, keyValues);
        }

        public virtual async Task<T> GetAsync(params object[] keyValues)
        {
            return await DbSet.FindAsync(keyValues);
        }

        public virtual async Task<T> GetAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await DbSet.FindAsync(cancellationToken, keyValues);
        }

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await GetAsync(cancellationToken, keyValues);

            if (entity == null)
                return false;
            ((DbContext)Context).Entry(entity).State = EntityState.Deleted;

            return true;
        }

        public virtual void SaveChangesAsny()
        {
            Context.SaveChangesAsync();
        }

        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }
    }
}
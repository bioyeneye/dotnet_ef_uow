using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CoreLibrary.Repositories;

namespace CoreLibrary.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        void Dispose(bool disposing);
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        void Commit();
        void Rollback();
    }
}
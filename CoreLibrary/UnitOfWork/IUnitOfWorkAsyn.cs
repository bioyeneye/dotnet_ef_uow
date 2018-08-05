using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CoreLibrary.Repositories;

namespace CoreLibrary.UnitOfWork
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;
    }
}
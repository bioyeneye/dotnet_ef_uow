using System;
using System.Threading;
using System.Threading.Tasks;
using CoreLibrary.DataContext;
using Microsoft.EntityFrameworkCore;

namespace CoreLibrary.EntityFramework
{
    public class EntityFrameworkDataContext<TContext> : DbContext, IDataContextAsync where TContext : DbContext
    {
        #region Private Fields
        private readonly Guid _instanceId;
        bool _disposed;
        #endregion Private Fields

        public EntityFrameworkDataContext(DbContextOptions<TContext> options)
            : base(options)
        {
            _instanceId = Guid.NewGuid();
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
            //Configuration.AutoDetectChangesEnabled = false;

        }

         
        public async Task<int> SaveChangesAsync()
        {
            return await this.SaveChangesAsync(CancellationToken.None);

        }
    }
}
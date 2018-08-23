using System;

namespace CoreLibrary.DataContext
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
    }
}
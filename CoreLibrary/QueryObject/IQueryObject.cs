using System;
using System.Linq.Expressions;

namespace CoreLibrary.QueryObject
{
    public interface IQueryObject<TEntity>
    {
        Expression<Func<TEntity,bool>> Expression { get; }
        IQueryObject<TEntity> And(Expression<Func<TEntity, bool>> query);
        IQueryObject<TEntity> Or(Expression<Func<TEntity, bool>> query);
        IQueryObject<TEntity> And(IQueryObject<TEntity> queryObject);
        IQueryObject<TEntity> Or(IQueryObject<TEntity> queryObject);

    }
}

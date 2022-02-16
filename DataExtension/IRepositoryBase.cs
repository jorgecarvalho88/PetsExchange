using System;
using System.Collections.Generic;
namespace DataExtension
{
    public interface IRepositoryBase
    {
        void BeginTransaction();
        int Commit();
        void CommitTransaction();
        TEntity Create<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(TEntity entity);
        IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class;
        void RollBackTransaction();
        void Update<TEntity>(TEntity entity);
    }
}

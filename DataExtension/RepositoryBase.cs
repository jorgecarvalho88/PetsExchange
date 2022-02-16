using Microsoft.EntityFrameworkCore;

namespace DataExtension
{
    public class RepositoryBase : IRepositoryBase
    {
        private DbContext context;

        public void BeginTransaction()
        {
            context.Database.BeginTransaction();
        }

        public RepositoryBase(DbContext context)
        {
            this.context = context;
        }

        public TEntity Create<TEntity>(TEntity entity) where TEntity : class
        {
            return context.Add(entity).Entity;
        }

        public void Update<TEntity>(TEntity entity)
        {
            context.Update(entity);
        }

        public void Delete<TEntity>(TEntity entity)
        {
            context.Remove(entity);
        }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public void CommitTransaction()
        {
            context.Database.CurrentTransaction?.Commit();
        }

        public void RollBackTransaction()
        {
            context.Database.CurrentTransaction?.Rollback();
        }

        public virtual IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class
        {
            return context.Set<TEntity>();
        }
    }
}

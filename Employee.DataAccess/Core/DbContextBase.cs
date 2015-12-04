using System;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Employee.DataAccess.Core
{
    public class DbContextBase : DbContext, IDbContext
    {
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void Edit<TEntity>(TEntity entity) where TEntity : class
        {
            this.Entry(entity).State = EntityState.Modified;
        }

        public void LoadReferenced<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty) 
            where TEntity : class
            where TProperty : class
        {
            this.Entry(entity).Reference(navigationProperty).Load();
        }
    }
}
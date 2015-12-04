using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Employee.DataAccess.Abstractions;
using Employee.DataAccess.Core;

using Guards;

namespace Employee.DataAccess.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly IDbSet<T> DbSet;
        private readonly IDbContext context;

        protected GenericRepository(IDbContext context)
        {
            Guard.ArgumentNotNull(() => context);

            this.context = context;

            this.DbSet = this.context.Set<T>();
        }

        public IContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IQueryable<T> Get(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = this.DbSet;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this.DbSet.AsEnumerable<T>();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = this.DbSet.Where(predicate).AsEnumerable();
            return query;
        }

        public virtual T Add(T entity)
        {
            return this.DbSet.Add(entity);
        }

        public virtual T Delete(T entity)
        {
            return this.DbSet.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
           this.Context.Edit(entity);
        }

        public virtual void LoadReferenced<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty)
            where TEntity : class
            where TProperty : class
        {
            this.Context.LoadReferenced(entity, navigationProperty);
        }

        public virtual void Save()
        {
            this.Context.SaveChanges();
        }
    }
}
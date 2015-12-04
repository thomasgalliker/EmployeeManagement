using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Employee.DataAccess.Abstractions
{
    public interface IGenericRepository<T>
    {
        IContext Context { get; }

        IQueryable<T> Get(params Expression<Func<T, object>>[] includes);

        IEnumerable<T> GetAll();

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        T Add(T entity);

        T Delete(T entity);

        void LoadReferenced<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty)
            where TEntity : class
            where TProperty : class;

        void Save();
    }
}
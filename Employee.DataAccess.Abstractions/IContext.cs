using System;

namespace Employee.DataAccess.Abstractions
{
    public interface IContext : IDisposable
    {
        void Edit<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}
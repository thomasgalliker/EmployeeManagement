using System;

namespace Employee.DataAccess.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        void RegisterContext<TContext>(TContext contextFactory) where TContext : IContext;

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int Commit();
    }
}

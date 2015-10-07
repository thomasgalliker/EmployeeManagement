using System.Data.Entity;

using Employee.DataAccess.Abstractions;

namespace Employee.DataAccess.Core
{
    public interface IDbContext : IContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class; 
    }
}
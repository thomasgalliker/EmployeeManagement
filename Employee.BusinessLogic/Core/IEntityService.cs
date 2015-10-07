using System.Collections.Generic;

namespace Employee.BusinessLogic.Core
{
    public interface IEntityService<T> where T : class
    {
        void Create(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        void Update(T entity);
    }
}

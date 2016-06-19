using EntityFramework.Toolkit.Core;

namespace Employee.DataAccess.Abstractions
{
    public interface IEmployeeRepository : IGenericRepository<Model.Employee>
    {
        Model.Employee GetById(long id);
    }
}

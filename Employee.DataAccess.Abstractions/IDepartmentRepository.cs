
namespace Employee.DataAccess.Abstractions
{
    public interface IDepartmentRepository : IGenericRepository<Model.Department>
    {
        Model.Department GetById(long id);
    }
}
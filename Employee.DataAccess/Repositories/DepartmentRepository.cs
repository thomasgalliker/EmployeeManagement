using System.Linq;

using Employee.DataAccess.Abstractions;
using Employee.Model;


namespace Employee.DataAccess.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IEmployeeContext context)
            : base(context)
        {
        }

        public Department GetById(long id)
        {
            return this.FindBy(x => x.Id == id).FirstOrDefault();
        }
    }
}
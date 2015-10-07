using System.Linq;

using Employee.DataAccess.Abstractions;

namespace Employee.DataAccess.Repositories
{
    public class EmployeeRepository : GenericRepository<Model.Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IEmployeeContext context)
            : base(context)
        {
        }

        ////public override IEnumerable<Model.Employee> GetAll()
        ////{
        ////    return this.context.Set<Model.Employee>()
        ////        ////.Include(x => x.Country)
        ////        .AsEnumerable();
        ////}

        public Model.Employee GetById(long id)
        {
            return this.DbSet.FirstOrDefault(x => x.Id == id);
        }
    }
}
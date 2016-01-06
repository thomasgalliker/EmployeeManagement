using System.Collections.Generic;

using Employee.Model;

namespace Employee.BusinessLogic.Abstractions
{
    public interface IEmployeeManager
    {
        ICollection<Model.Employee> GetAllEmployees(); // BUG: Should be ICollection instead of IEnumerable

        Model.Employee GetEmployeeById(long id);

        Model.Employee CreateEmployee(Model.Employee employee);

        ICollection<Department> GetAllDepartments(); // BUG: Should be ICollection instead of IEnumerable

        void CreateDepartment(Department department);
    }
}
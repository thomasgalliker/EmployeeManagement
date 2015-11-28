using System.Collections.Generic;

using Employee.Model;

namespace Employee.BusinessLogic.Abstractions
{
    public interface IEmployeeManager
    {
        IEnumerable<Model.Employee> GetAllEmployees(); // BUG: Should be ICollection instead of IEnumerable

        Model.Employee CreateEmployee(Model.Employee employee);

        IEnumerable<Department> GetAllDepartments(); // BUG: Should be ICollection instead of IEnumerable

        void CreateDepartment(Department department);
    }
}
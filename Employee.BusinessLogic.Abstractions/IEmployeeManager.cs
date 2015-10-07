using System.Collections.Generic;

using Employee.Model;

namespace Employee.BusinessLogic.Abstractions
{
    public interface IEmployeeManager
    {
        IEnumerable<Model.Employee> GetAllEmployees();

        void CreateEmployee(Model.Employee employee);

        IEnumerable<Department> GetAllDepartments();

        void CreateDepartment(Department department);
    }
}
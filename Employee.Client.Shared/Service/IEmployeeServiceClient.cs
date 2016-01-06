
using System.Collections.Generic;
using System.Threading.Tasks;

using Employee.Service.Contracts.DataContracts;

namespace Employee.Client.Shared.Service
{
    public interface IEmployeeServiceClient
    {
        Task<IList<EmployeeDto>> GetAllEmployees();

        Task<EmployeeDto> GetEmployeeById(long id);

        Task CreateEmployee(EmployeeDto employee);

        Task<IList<DepartmentDto>> GetAllDepartments();

        Task CreateDepartment(DepartmentDto department);
    }
}

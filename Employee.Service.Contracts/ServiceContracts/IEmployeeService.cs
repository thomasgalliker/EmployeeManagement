using System.Collections.Generic;
using System.ServiceModel;

using Employee.Service.Contracts.DataContracts;

namespace Employee.Service.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        void CreateEmployee(EmployeeDto employee);

        [OperationContract]
        IEnumerable<EmployeeDto> GetAllEmployees();

        [OperationContract]
        void CreateDepartment(DepartmentDto department);

        [OperationContract]
        IEnumerable<DepartmentDto> GetAllDepartments();
    }
}

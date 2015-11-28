
using System.Collections.Generic;

using Employee.BusinessLogic.Abstractions;
using Employee.DataAccess.Abstractions;
using Employee.Model;

using Guards;

namespace Employee.BusinessLogic
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IDepartmentRepository departmentRepository;

        public EmployeeManager(
            IUnitOfWork unitOfWork,
            IEmployeeRepository employeeRepository, 
            IDepartmentRepository departmentRepository)
        {
            Guard.ArgumentNotNull(() => unitOfWork);
            Guard.ArgumentNotNull(() => employeeRepository);
            Guard.ArgumentNotNull(() => departmentRepository);

            this.employeeRepository = employeeRepository;
            this.departmentRepository = departmentRepository;

            this.unitOfWork = unitOfWork;
            this.unitOfWork.RegisterContext(employeeRepository.Context);
            this.unitOfWork.RegisterContext(departmentRepository.Context); // BUG: They both share the same context; no need to register 2x
        }

        public IEnumerable<Model.Employee> GetAllEmployees()
        {
            return this.employeeRepository.GetAll();
        }

        public Model.Employee CreateEmployee(Model.Employee employee)
        {
            Guard.ArgumentNotNull(() => employee);

            this.employeeRepository.Add(employee);
            this.unitOfWork.Commit();

            return employee;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return this.departmentRepository.GetAll();
        }

        public void CreateDepartment(Department department)
        {
            Guard.ArgumentNotNull(() => department);

            foreach (var employee in department.Employees)
            {
                this.employeeRepository.Add(employee);
            }

            this.departmentRepository.Add(department);

            this.unitOfWork.Commit();
        }
    }
}
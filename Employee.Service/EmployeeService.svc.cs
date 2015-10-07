using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Wcf;

using Employee.BusinessLogic.Abstractions;
using Employee.Model;
using Employee.Service.Contracts.DataContracts;
using Employee.Service.Contracts.ServiceContracts;

namespace Employee.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EmployeeService : IEmployeeService
    {
        public EmployeeService()
        {
        }

        public void CreateEmployee(EmployeeDto employeeDto)
        {
            try
            {
                using (var httpRequestScope = AutofacHostFactory.Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
                {
                    var employeeService = httpRequestScope.Resolve<IEmployeeManager>();

                    var employee = this.ConvertToEntity(employeeDto);
                    employeeService.CreateEmployee(employee);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            try
            {
                using (var httpRequestScope = AutofacHostFactory.Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
                {
                    var employeeService = httpRequestScope.Resolve<IEmployeeManager>();

                    var allEmployees = employeeService.GetAllEmployees();
                    return this.ConvertToDto(allEmployees);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public void CreateDepartment(DepartmentDto departmentDto)
        {
            try
            {
                using (var httpRequestScope = AutofacHostFactory.Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
                {
                    var employeeService = httpRequestScope.Resolve<IEmployeeManager>();

                    var department = this.ConvertToEntity(departmentDto);
                    employeeService.CreateDepartment(department);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        private Department ConvertToEntity(DepartmentDto departmentDto)
        {
            return new Department
                       {
                           Name = departmentDto.Name, 
                           Leader = this.ConvertToEntity(departmentDto.Leader),
                           Employees = departmentDto.Employees.Select(this.ConvertToEntity).ToList()
                       };
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            try
            {
                using (var httpRequestScope = AutofacHostFactory.Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
                {
                    var employeeService = httpRequestScope.Resolve<IEmployeeManager>();

                    var allDepartments = employeeService.GetAllDepartments();
                    return this.ConvertToDto(allDepartments);
                }
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        private Model.Employee ConvertToEntity(EmployeeDto employee)
        {
            return new Model.Employee
                        {
                            FirstName = employee.FirstName,
                            LastName = employee.LastName, 
                            Birthdate = employee.Birthdate
                        };
        }

        private IEnumerable<EmployeeDto> ConvertToDto(IEnumerable<Model.Employee> allEmployees)
        {
            return allEmployees.Select(this.ConvertToDto).ToList();
        }
        
        private EmployeeDto ConvertToDto(Model.Employee employee)
        {
            return  new EmployeeDto
                    {
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Birthdate = employee.Birthdate.Value
                    };
        }

        private IEnumerable<DepartmentDto> ConvertToDto(IEnumerable<Model.Department> allDepartments)
        {
            return allDepartments.Select(department =>
                new DepartmentDto
                {
                    Name = department.Name,
                    Leader = this.ConvertToDto(department.Leader),
                    Employees = department.Employees.Select(this.ConvertToDto).ToList()
                }).ToList();
        }
    }
}

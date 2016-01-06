using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;

using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Wcf;

using Employee.BusinessLogic.Abstractions;
using Employee.Mapping.Abstraction;
using Employee.Model;
using Employee.Service.Contracts.DataContracts;
using Employee.Service.Contracts.ServiceContracts;

namespace Employee.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper<Model.Employee, EmployeeDto> employeeToDtoMapper;
        private readonly IMapper<EmployeeDto, Model.Employee> dtoToEmployeeMapper;

        public EmployeeService(
            IMapper<Model.Employee, EmployeeDto> employeeToDtoMapper,
            IMapper<EmployeeDto, Model.Employee> dtoToEmployeeMapper)
        {
            this.employeeToDtoMapper = employeeToDtoMapper;
            this.dtoToEmployeeMapper = dtoToEmployeeMapper;
        }

        [WebInvoke(Method = "POST")]
        public void CreateEmployee(EmployeeDto employeeDto)
        {
            try
            {
                using (var httpRequestScope = AutofacHostFactory.Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
                {
                    var employeeService = httpRequestScope.Resolve<IEmployeeManager>();

                    var employee = this.dtoToEmployeeMapper.Map(employeeDto);
                    employeeService.CreateEmployee(employee);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        //[WebGet(UriTemplate = "/getallemployees")] // UriTemplates are not supported in our custom message formatter
        [WebGet]
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

        [WebInvoke(Method = "POST")]
        public EmployeeDto GetEmployeeById(long id)
        {
            try
            {
                using (var httpRequestScope = AutofacHostFactory.Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
                {
                    var employeeService = httpRequestScope.Resolve<IEmployeeManager>();

                    var employee = employeeService.GetEmployeeById(id);
                    return this.employeeToDtoMapper.Map(employee);
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
                           Leader = this.dtoToEmployeeMapper.Map(departmentDto.Leader),
                           Employees = departmentDto.Employees.Select(this.dtoToEmployeeMapper.Map).ToList()
                       };
        }

        [WebGet]
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

        private IEnumerable<EmployeeDto> ConvertToDto(IEnumerable<Model.Employee> allEmployees)
        {
            return allEmployees.Select(this.employeeToDtoMapper.Map).ToList();
        }

        private IEnumerable<DepartmentDto> ConvertToDto(IEnumerable<Model.Department> allDepartments)
        {
            return allDepartments.Select(department =>
                new DepartmentDto
                {
                    Id = department.Id,
                    Name = department.Name,
                    Leader = this.employeeToDtoMapper.Map(department.Leader),
                    Employees = department.Employees.Select(this.employeeToDtoMapper.Map).ToList()
                }).ToList();
        }
    }
}

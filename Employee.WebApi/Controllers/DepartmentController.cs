using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using Employee.BusinessLogic.Abstractions;
using Employee.Mapping.Abstraction;
using Employee.Service.Contracts.DataContracts;

using Guards;

namespace Employee.WebApi.Controllers
{
    public class DepartmentController : ApiController
    {
        private readonly IEmployeeManager employeeManager;
        private readonly IMapper<DepartmentDto, Model.Department> dtoToDepartmentMapper;
        private readonly IMapper<Model.Department, DepartmentDto> departmentToDtoMapper;

        public DepartmentController(
            IEmployeeManager employeeManager,
            IMapper<DepartmentDto, Model.Department> dtoToDepartmentMapper,
            IMapper<Model.Department, DepartmentDto> departmentToDtoMapper)
        {
            Guard.ArgumentNotNull(() => employeeManager);
            Guard.ArgumentNotNull(() => dtoToDepartmentMapper);
            Guard.ArgumentNotNull(() => departmentToDtoMapper);

            this.employeeManager = employeeManager;
            this.dtoToDepartmentMapper = dtoToDepartmentMapper;
            this.departmentToDtoMapper = departmentToDtoMapper;
        }

        // GET api/department
        public IEnumerable<DepartmentDto> Get()
        {
            var allDepartments = this.employeeManager.GetAllDepartments();
            var departmentDtos = this.ConvertToDto(allDepartments);
            return departmentDtos;
        }

        private IEnumerable<DepartmentDto> ConvertToDto(IEnumerable<Model.Department> allDepartments)
        {
            return allDepartments.Select(this.departmentToDtoMapper.Map).ToList();
        }

        // GET api/department/5
        public string Get(int id)
        {
            var singleOrDefault = this.employeeManager.GetAllEmployees().SingleOrDefault(e => e.Id == id);
            return "value";
        }

        // POST api/department
        public void Post([FromBody]string value)
        {
        }

        // PUT api/department/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/department/5
        public void Delete(int id)
        {
        }
    }
}

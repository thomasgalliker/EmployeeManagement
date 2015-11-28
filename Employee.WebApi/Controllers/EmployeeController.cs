using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

using Employee.BusinessLogic.Abstractions;
using Employee.Mapping.Abstraction;
using Employee.Service.Contracts.DataContracts;

using Guards;

namespace Employee.WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeManager employeeManager;
        private readonly IMapper<EmployeeDto, Model.Employee> dtoToEmployeeMapper;
        private readonly IMapper<Model.Employee, EmployeeDto> employeeToDtoMapper;

        public EmployeeController(
            IEmployeeManager employeeManager, 
            IMapper<EmployeeDto, Model.Employee> dtoToEmployeeMapper, 
            IMapper<Model.Employee, EmployeeDto> employeeToDtoMapper)
        {
            Guard.ArgumentNotNull(() => employeeManager);
            Guard.ArgumentNotNull(() => dtoToEmployeeMapper);
            Guard.ArgumentNotNull(() => employeeToDtoMapper);

            this.employeeManager = employeeManager;
            this.dtoToEmployeeMapper = dtoToEmployeeMapper;
            this.employeeToDtoMapper = employeeToDtoMapper;
        }

        // GET api/employee
        public IEnumerable<EmployeeDto> Get()
        {
            var allEmployees = this.employeeManager.GetAllEmployees();

            return this.ConvertToDto(allEmployees);
        }

        private IEnumerable<EmployeeDto> ConvertToDto(IEnumerable<Model.Employee> allEmployees)
        {
            return allEmployees.Select(x => this.employeeToDtoMapper.Map(x)).ToList();
        }

        // GET api/employee/5
        [ResponseType(typeof(EmployeeDto))]
        public IHttpActionResult Get(int id)
        {
            var employeeDto = this.employeeManager.GetAllEmployees().SingleOrDefault(e => e.Id == id);
            if (employeeDto == null)
            {
                return this.NotFound();
            }

            return this.Ok(this.employeeToDtoMapper.Map(employeeDto));
        }

        // POST api/employee
        [ResponseType(typeof(EmployeeDto))]
        public IHttpActionResult Post([FromBody] EmployeeDto employeeDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var employee = this.dtoToEmployeeMapper.Map(employeeDto);

            var employeeWithId = this.employeeManager.CreateEmployee(employee);
            var employeeDtoWithId = this.employeeToDtoMapper.Map(employeeWithId);

            return this.CreatedAtRoute("DefaultApi", new { id = employeeDtoWithId.Id }, employeeDtoWithId);
        }

        // PUT api/employee/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/employee/5
        public void Delete(int id)
        {
        }
    }
}
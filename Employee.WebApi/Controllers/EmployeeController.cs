using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

using Employee.BusinessLogic.Abstractions;
using Employee.Mapping.Abstraction;
using Employee.Service.Contracts.DataContracts;
using Employee.WebApi.ExceptionHandling;

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

        // GET api/employee/all
        [ActionName("all")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<EmployeeDto>))]
        public IHttpActionResult Get()
        {
            var allEmployees = this.employeeManager.GetAllEmployees();

            var employeeDtos = this.ConvertToDto(allEmployees);

            return this.Ok(employeeDtos);
        }

        // GET api/employee/provokeError
        [Route("api/employee/provokeError")]
        [ActionName("provokeError")]
        [HttpGet]
        public IHttpActionResult ProvokeError()
        {
            // Here we simulate a known exception being thrown...
            throw new UserProvokedException("A known error has been provoked by the client!");
        }

        [Route("api/employee/provokeUnknownError")]
        [ActionName("provokeUnknownError")]
        [HttpGet]
        public IHttpActionResult ProvokeUnknownError()
        {
            // Here we simulate an unknown exception being thrown...
            throw new InvalidOperationException("An unknown error has been provoked by the client!");
        }

        private IEnumerable<EmployeeDto> ConvertToDto(IEnumerable<Model.Employee> allEmployees)
        {
            return allEmployees.Select(x => this.employeeToDtoMapper.Map(x)).ToList();
        }

        // GET api/employee/5
        [ResponseType(typeof(EmployeeDto))]
        public IHttpActionResult Get(int id)
        {
            var employee = this.employeeManager.GetAllEmployees().SingleOrDefault(e => e.Id == id);
            if (employee == null)
            {
                var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Could not find employee with ID {0}.", id)),
                    ReasonPhrase = "Employee not found."
                };
                throw new HttpResponseException(httpResponseMessage);
            }

            var employeeDto = this.employeeToDtoMapper.Map(employee);

            return this.Ok(employeeDto);
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;

using Employee.BusinessLogic.Abstractions;

namespace Employee.WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeManager employeeManager;

        public EmployeeController(IEmployeeManager employeeManager)
        {
            this.employeeManager = employeeManager;
        }

        // GET api/employee
        public IEnumerable<string> Get()
        {
            this.employeeManager.CreateEmployee(new Model.Employee { FirstName = "Thomas", LastName = "Galliker", Birthdate = DateTime.Now});
            var allEmployees = this.employeeManager.GetAllEmployees();
            foreach (var e in allEmployees)
            {
                Debug.WriteLine(e);
            }
            return new string[] { "value1", "value2" };
        }

        // GET api/employee/5
        public string Get(int id)
        {
            var singleOrDefault = this.employeeManager.GetAllEmployees().SingleOrDefault(e => e.Id == id);
            return "value";
        }

        // POST api/employee
        public void Post([FromBody]string value)
        {
        }

        // PUT api/employee/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/employee/5
        public void Delete(int id)
        {
        }
    }
}

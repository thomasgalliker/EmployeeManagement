using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

using Employee.Model;

namespace Employee.DataAccess.Utils
{
    /// <summary>
    /// EmployeeContextInitializer seeds the employee context with data
    /// generated from http://www.generatedata.com/.
    /// </summary>
    public class EmployeeContextInitializer : DropCreateDatabaseAlways<EmployeeContext>
    {
        protected override void Seed(EmployeeContext context)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var departmentResource = "Employee.DataAccess.Seed.Departments.xml";
            var departmentStream = assembly.GetManifestResourceStream(departmentResource);
            var departmentXml = XDocument.Load(departmentStream);
            var departmentsElement = departmentXml.Element("departments");
            if (departmentsElement != null)
            {
                var departments = departmentsElement.Elements("department").Select(x =>
                    new Department
                    {
                        Id = (int)x.Element("Id"),
                        Name = (string)x.Element("Name"),
                    }).ToArray();

                var dbSet = context.Set<Department>();
                foreach (var department in departments)
                {
                    dbSet.Add(department);
                }
            }

            var employeesResource = "Employee.DataAccess.Seed.Employees.xml";
            var stream = assembly.GetManifestResourceStream(employeesResource);
            var employeesXml = XDocument.Load(stream);
            var employeesElement = employeesXml.Element("employees");
            if (employeesElement != null)
            {
                var employees = employeesElement.Elements("employee").Select(x => 
                    new Model.Employee
                        {
                            Id = (int)x.Element("Id"),
                            FirstName = (string)x.Element("FirstName"),
                            LastName = (string)x.Element("LastName"),
                            Birthdate = DateTime.Parse((string)x.Element("Birthdate")),
                            DepartmentId = (int)x.Element("DepartmentId"),
                        }).ToArray();

                var dbSet = context.Set<Model.Employee>();
                foreach (var employee in employees)
                {
                    dbSet.Add(employee);
                }
            }

            base.Seed(context);
        }
    }
   
}

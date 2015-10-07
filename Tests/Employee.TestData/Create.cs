using System;
using System.Collections.Generic;

using Employee.Service.Contracts.DataContracts;

namespace Employee.TestData
{
    public static class CreateEntity
    {
        public static readonly Model.Employee Employee1 = new Model.Employee { FirstName = "Thomas", LastName = "Galliker", Birthdate = new DateTime(1986, 07, 11) };
        public static readonly Model.Employee Employee2 = new Model.Employee { FirstName = "Fritz", LastName = "Müller", Birthdate = new DateTime(1990, 01, 01) };
        public static readonly Model.Employee Employee3 = new Model.Employee { FirstName = "Lorem", LastName = "Ipsum", Birthdate = new DateTime(2000, 12, 31) };

        public static readonly Model.Department Department1 = new Model.Department { Name = "Human Resources", Leader = Employee1, Employees = new List<Model.Employee> { Employee2, Employee3 } };
        public static readonly Model.Department Department2 = new Model.Department { Name = "Development", Leader = Employee2, Employees = new List<Model.Employee> { Employee1, Employee3 } };
    }

    public static class CreateDto
    {
        public static readonly EmployeeDto Employee1 = new EmployeeDto { FirstName = "Thomas", LastName = "Galliker", Birthdate = new DateTime(1986, 07, 11) };
        public static readonly EmployeeDto Employee2 = new EmployeeDto { FirstName = "Fritz", LastName = "Müller", Birthdate = new DateTime(1990, 01, 01) };
        public static readonly EmployeeDto Employee3 = new EmployeeDto { FirstName = "Lorem", LastName = "Ipsum", Birthdate = new DateTime(2000, 12, 31) };

        public static readonly DepartmentDto Department1 = new DepartmentDto { Name = "Human Resources", Leader = Employee1, Employees = new List<EmployeeDto> { Employee2, Employee3 } };
        public static readonly DepartmentDto Department2 = new DepartmentDto { Name = "Development", Leader = Employee2, Employees = new List<EmployeeDto> { Employee1, Employee3 } };
    }
}

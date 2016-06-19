
using System.Linq;

using Employee.DataAccess.Abstractions;
using Employee.DataAccess.Repositories;
using Employee.TestData;

using FluentAssertions;

using Xunit;

namespace Employee.DataAccess.Tests
{
    [Collection("DepartmentRepository")]
    public class DepartmentRepositoryTests
    {
        [Fact]
        public void ShouldReturnEmptyGetAll()
        {
            // Arrange
            using (IEmployeeContext employeeContext = new EmployeeContext())
            {
                var departmentRepository = new DepartmentRepository(employeeContext);

                // Act
                var allDepartments = departmentRepository.GetAll().ToList();

                 // Assert
                allDepartments.Should().HaveCount(0);
            }
        }

        [Fact]
        public void ShouldAddDepartment()
        {
            // Arrange
            using (IEmployeeContext employeeContext = new EmployeeContext())
            {
                IEmployeeRepository employeeRepository = new EmployeeRepository(employeeContext);
                employeeRepository.Add(CreateEntity.Employee1);
                employeeRepository.Add(CreateEntity.Employee2);
                employeeRepository.Add(CreateEntity.Employee3);
                employeeContext.SaveChanges();

                IDepartmentRepository departmentRepository = new DepartmentRepository(employeeContext);
                var department = CreateEntity.Department1;

                // Act
                departmentRepository.Add(department);
                var numberOfChangesCommitted = employeeContext.SaveChanges();

                // Assert
                numberOfChangesCommitted.Should().BeGreaterThan(0);

                var allDepartments = departmentRepository.GetAll().ToList();
                allDepartments.Should().HaveCount(1);
                allDepartments.ElementAt(0).ShouldBeEquivalentTo(department, options => options.IncludingAllDeclaredProperties());
            }
        }

        [Fact]
        public void ShouldDeleteDepartment()
        {
            // Arrange
            using (IEmployeeContext employeeContext = new EmployeeContext())
            {
                IDepartmentRepository departmentRepository = new DepartmentRepository(employeeContext);
                var department1 = CreateEntity.Department1;
                var department2 = CreateEntity.Department2;

                departmentRepository.Add(department1);
                departmentRepository.Add(department2);
                var numberOfAdds = +employeeContext.SaveChanges();

                // Act
                departmentRepository.Remove(department2);
                var numberOfDeletes =+ employeeContext.SaveChanges();

                // Assert
                numberOfAdds.Should().BeGreaterThan(0); ;
                numberOfDeletes.Should().BeGreaterThan(0);

                var allDepartments = departmentRepository.GetAll().ToList();
                allDepartments.Should().HaveCount(1);
                allDepartments.ElementAt(0).ShouldBeEquivalentTo(department1, options => options.IncludingAllDeclaredProperties());
            }
        }
    }
}
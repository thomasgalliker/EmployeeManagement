
using Employee.BusinessLogic.Abstractions;
using Employee.DataAccess.Abstractions;
using Employee.TestData;

using Moq;

using Xunit;

namespace Employee.BusinessLogic.Tests
{
    public class EmployeeManagerTests
    {
        [Fact]
        public void ShouldCreateEmployee()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var departmentRepositoryMock = new Mock<IDepartmentRepository>();

            IEmployeeManager employeeManager = new EmployeeManager(unitOfWorkMock.Object, employeeRepositoryMock.Object, departmentRepositoryMock.Object);
            var employee = CreateEntity.Employee1;

            // Act
            employeeManager.CreateEmployee(employee);

            // Assert
            unitOfWorkMock.Verify(r => r.Commit(), Times.Once);
            employeeRepositoryMock.Verify(r => r.Add(It.IsAny<Model.Employee>()), Times.Once);
            departmentRepositoryMock.Verify(r => r.Add(It.IsAny<Model.Department>()), Times.Never);
        }

        [Fact]
        public void ShouldCreateDepartmentWithEmployees()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var departmentRepositoryMock = new Mock<IDepartmentRepository>();

            IEmployeeManager employeeManager = new EmployeeManager(unitOfWorkMock.Object, employeeRepositoryMock.Object, departmentRepositoryMock.Object);
            var department = CreateEntity.Department1;

            // Act
            employeeManager.CreateDepartment(department);

            // Assert
            unitOfWorkMock.Verify(r => r.Commit(), Times.Once);
            employeeRepositoryMock.Verify(r => r.Add(It.IsAny<Model.Employee>()), Times.Exactly(department.Employees.Count));
            departmentRepositoryMock.Verify(r => r.Add(It.IsAny<Model.Department>()), Times.Once);
        }
    }
}

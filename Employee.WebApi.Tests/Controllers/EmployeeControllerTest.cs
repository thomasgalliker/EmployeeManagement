using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

using Employee.BusinessLogic.Abstractions;
using Employee.Mapping.Abstraction;
using Employee.Service.Contracts.DataContracts;
using Employee.WebApi.Controllers;

using FluentAssertions;
using Moq;

using Xunit;

namespace Employee.WebApi.Tests.Controllers
{
    public class EmployeeControllerTest
    {
        [Fact]
        public void Get()
        {
            // Arrange
            var employeeList = new List<Model.Employee>
            {
                new Model.Employee { FirstName = "James", LastName = "Last" },
                new Model.Employee { FirstName = "Hoho", LastName = "Gugu" },
            };
            var employeeManagerMock = new Mock<IEmployeeManager>();
            employeeManagerMock.Setup(m => m.GetAllEmployees()).Returns(employeeList);

            var dtoToEmployeeMapperMock = new Mock<IMapper<EmployeeDto, Model.Employee>>();

            var employeeToDtoMapperMock = new Mock<IMapper<Model.Employee, EmployeeDto>>();
            employeeToDtoMapperMock.Setup(m => m.Map(It.IsAny<Model.Employee>()))
                .Returns<Model.Employee>(entity => new EmployeeDto { FirstName = entity.FirstName, LastName = entity.LastName });

            var employeeController = new EmployeeController(
                employeeManagerMock.Object,
                dtoToEmployeeMapperMock.Object,
                employeeToDtoMapperMock.Object);

            // Act
            var actionResult = employeeController.Get();

            // Assert
            var contentResult = Assert.IsType<OkNegotiatedContentResult<IEnumerable<EmployeeDto>>>(actionResult);

            contentResult.Content.Should().HaveCount(employeeList.Count);
            contentResult.Content.ElementAt(0).FirstName.Should().Be("James");
            contentResult.Content.ElementAt(0).LastName.Should().Be("Last");
            contentResult.Content.ElementAt(1).FirstName.Should().Be("Hoho");
            contentResult.Content.ElementAt(1).LastName.Should().Be("Gugu");
        }

        ////[Fact]
        ////public void GetById()
        ////{
        ////    // Arrange
        ////    var employeeManagerMock = new Mock<IEmployeeManager>();
        ////    var employeeController = new EmployeeController(employeeManagerMock.Object);

        ////    // Act
        ////    string result = employeeController.Get(5);

        ////    // Assert
        ////    Assert.Equal("value", result);
        ////}

        [Fact]
        public void Post()
        {
            // Arrange
            var employee = new EmployeeDto { FirstName = "James", LastName = "Last" };
            int employeeId = 99;

            var employeeManagerMock = new Mock<IEmployeeManager>();
            employeeManagerMock.Setup(m => m.CreateEmployee(It.IsAny<Model.Employee>())).Returns<Model.Employee>(
                entity =>
                    {
                        entity.Id = employeeId;
                        return entity;
                    });

            var dtoToEmployeeMapperMock = new Mock<IMapper<EmployeeDto, Model.Employee>>();
            dtoToEmployeeMapperMock.Setup(m => m.Map(It.IsAny<EmployeeDto>()))
                .Returns<EmployeeDto>(dto => new Model.Employee { Id = dto.Id, FirstName = dto.FirstName, LastName = dto.LastName });

            var employeeToDtoMapperMock = new Mock<IMapper<Model.Employee, EmployeeDto>>();
            employeeToDtoMapperMock.Setup(m => m.Map(It.IsAny<Model.Employee>()))
                .Returns<Model.Employee>(entity => new EmployeeDto { Id = entity.Id, FirstName = entity.FirstName, LastName = entity.LastName });

            var employeeController = new EmployeeController(
                employeeManagerMock.Object,
                dtoToEmployeeMapperMock.Object,
                employeeToDtoMapperMock.Object);

            // Act
            var actionResult = employeeController.Post(employee);

            // Assert
            var contentResult = Assert.IsType<CreatedAtRouteNegotiatedContentResult<EmployeeDto>>(actionResult);

            contentResult.Content.Id.Should().Be(employeeId);
            contentResult.Content.FirstName.Should().Be("James");
            contentResult.Content.LastName.Should().Be("Last");
        }

        ////[Fact]
        ////public void Put()
        ////{
        ////    // Arrange
        ////    var employeeManagerMock = new Mock<IEmployeeManager>();
        ////    var employeeController = new EmployeeController(employeeManagerMock.Object);

        ////    // Act
        ////    employeeController.Put(5, "value");

        ////    // Assert
        ////}

        ////[Fact]
        ////public void Delete()
        ////{
        ////    // Arrange
        ////    var employeeManagerMock = new Mock<IEmployeeManager>();
        ////    var employeeController = new EmployeeController(employeeManagerMock.Object);

        ////    // Act
        ////    employeeController.Delete(5);

        ////    // Assert
        ////}
    }
}
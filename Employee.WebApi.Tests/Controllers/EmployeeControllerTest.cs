using System.Collections.Generic;
using System.Linq;

using Employee.BusinessLogic.Abstractions;
using Employee.WebApi.Controllers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Employee.WebApi.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            var employeeManagerMock = new Mock<IEmployeeManager>();
            var employeeController = new EmployeeController(employeeManagerMock.Object);

            // Act
            IEnumerable<string> result = employeeController.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            var employeeManagerMock = new Mock<IEmployeeManager>();
            var employeeController = new EmployeeController(employeeManagerMock.Object);

            // Act
            string result = employeeController.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            var employeeManagerMock = new Mock<IEmployeeManager>();
            var employeeController = new EmployeeController(employeeManagerMock.Object);

            // Act
            employeeController.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            var employeeManagerMock = new Mock<IEmployeeManager>();
            var employeeController = new EmployeeController(employeeManagerMock.Object);

            // Act
            employeeController.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            var employeeManagerMock = new Mock<IEmployeeManager>();
            var employeeController = new EmployeeController(employeeManagerMock.Object);

            // Act
            employeeController.Delete(5);

            // Assert
        }
    }
}
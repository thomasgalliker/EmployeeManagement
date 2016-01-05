using System.Web.Mvc;

using Employee.WebApi.Controllers;

using Xunit;

namespace Employee.WebApi.Tests.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            ////Assert.IsNotNull(result);
            ////Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}

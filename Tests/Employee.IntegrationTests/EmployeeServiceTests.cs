

using System;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;

using Employee.DataAccess;
using Employee.Service;

using FluentAssertions;

using Xunit;

using IEmployeeService = Employee.Service.Contracts.ServiceContracts.IEmployeeService;

namespace Employee.IntegrationTests
{
    public class EmployeeServiceTests : IDisposable
    {
        private readonly ServiceHost serviceHost;
        private readonly ServiceEndpoint endpoint;

        public EmployeeServiceTests()
        {
            // Setup WCF host
            this.serviceHost = new ServiceHost(typeof(EmployeeService));
            this.serviceHost.Opened += (sender, args) => { Bootstrapper.RegisterIoc(); };

            this.endpoint = this.serviceHost.AddServiceEndpoint(
                typeof(IEmployeeService),
                new BasicHttpBinding(),
                "http://localhost:2020/EmployeeService.svc"); 
            this.serviceHost.Open();

            // Setup database
            AppDomain.CurrentDomain.SetData("DataDirectory", @"C:\src\EmployeeManagement\Employee.Service\App_Data");
            using (var context = new EmployeeContext())
            {
                Database.SetInitializer(
                    new ForceDeleteInitializer<EmployeeContext>(
                        new DropCreateDatabaseAlways<EmployeeContext>()));

                context.Database.Initialize(force: true);
            }
        }

        [Fact]
        public void ShouldCreateDepartment()
        {
            // Arrange
            var channelFactory = new ChannelFactory<IEmployeeService>(this.endpoint);
            var employeeServiceClient = channelFactory.CreateChannel();

            var department = TestData.CreateDto.Department1;

            // Act
            employeeServiceClient.CreateDepartment(department);

            // Assert
            var allDepartments = employeeServiceClient.GetAllDepartments();
            allDepartments.Should().HaveCount(1);
            allDepartments.ElementAt(0).ShouldBeEquivalentTo(department, options => options.IncludingAllDeclaredProperties());
        }

        [Fact]
        public void ShouldNotCreateDepartmentIfAlreadyExists()
        {
            // Arrange
            var channelFactory = new ChannelFactory<IEmployeeService>(this.endpoint);
            var employeeServiceClient = channelFactory.CreateChannel();
            var department = TestData.CreateDto.Department1;

            // Act
            employeeServiceClient.CreateDepartment(department);
            Assert.Throws<FaultException>(() => employeeServiceClient.CreateDepartment(department));

            // Assert
            var allDepartments = employeeServiceClient.GetAllDepartments();
            allDepartments.Should().HaveCount(1);
            allDepartments.ElementAt(0).ShouldBeEquivalentTo(department, options => options.IncludingAllDeclaredProperties());
        }

        public void Dispose()
        {
            if (this.serviceHost != null)
            {
                this.serviceHost.Close();
            }
        }
    }
}

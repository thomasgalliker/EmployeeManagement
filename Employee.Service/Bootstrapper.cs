using System.Reflection;

using Autofac;
using Autofac.Integration.Wcf;

namespace Employee.Service
{
    internal static class Bootstrapper
    {
        internal static void RegisterIoc()
        {
            var builder = new ContainerBuilder();

            // Register your service implementations.
            builder.RegisterType<EmployeeService>().SingleInstance();

            builder.RegisterAssemblyModules(Assembly.Load("Employee.DataAccess"));
            builder.RegisterAssemblyModules(Assembly.Load("Employee.BusinessLogic"));
            builder.RegisterAssemblyModules(Assembly.Load("Employee.Mapping"));

            // Set the dependency resolver.
            var container = builder.Build();
            AutofacHostFactory.Container = container;
        }
    }
}
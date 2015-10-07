
using Autofac;

using Employee.DataAccess.Abstractions;
using Employee.DataAccess.Core;
using Employee.DataAccess.Repositories;

namespace Employee.DataAccess.Configuration
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ////builder.RegisterAssemblyTypes(typeof(ContextProvider<>).Assembly).AsClosedTypesOf(typeof(IContextProvider<>)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(EmployeeContext)).As(typeof(IEmployeeContext)).InstancePerLifetimeScope();

            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>().InstancePerLifetimeScope();

            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();  
 
           
        }
    }
}

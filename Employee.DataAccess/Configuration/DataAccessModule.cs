using Autofac;

using Employee.DataAccess.Abstractions;
using Employee.DataAccess.Repositories;

using EntityFramework.Toolkit;
using EntityFramework.Toolkit.Core;

namespace Employee.DataAccess.Configuration
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(EmployeeContext)).As(typeof(IEmployeeContext)).InstancePerLifetimeScope();

            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>().InstancePerLifetimeScope();

            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();  
        }
    }
}

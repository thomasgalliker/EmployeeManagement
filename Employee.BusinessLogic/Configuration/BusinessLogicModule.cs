using Autofac;

using Employee.BusinessLogic.Abstractions;

namespace Employee.BusinessLogic.Configuration
{
    public class BusinessLogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeManager>().As<IEmployeeManager>().InstancePerRequest();
        }
    }
}

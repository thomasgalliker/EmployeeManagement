using Autofac;

namespace Employee.Mapping.Configuration
{
    public class MappingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            AutoMapperConfiguration.Configure(builder);
        }
    }
}

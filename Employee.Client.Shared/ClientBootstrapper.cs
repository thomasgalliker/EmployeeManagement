
using CrossPlatformLibrary.Bootstrapping;
using CrossPlatformLibrary.IoC;

using Employee.Client.Shared.Service;
using Employee.Client.Shared.ViewModel;

namespace Employee.Client.Shared
{
    public class ClientBootstrapper : Bootstrapper
    {
        protected override void ConfigureContainer(ISimpleIoc container)
        {
            base.ConfigureContainer(container);

            container.Register<IEmployeeServiceClient, EmployeeServiceClient>();

            // Registering view models
            container.Register<MainViewModel>();
        }
    }
}

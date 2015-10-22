using System.Windows;

using CrossPlatformLibrary.Bootstrapping;

using Employee.Client.Shared;

namespace Employee.Client.WPF
{
    public partial class App : Application
    {
        private readonly Bootstrapper bootstrapper;

        public App()
        {
            this.bootstrapper = new ClientBootstrapper();
            this.bootstrapper.Startup();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.bootstrapper.Shutdown();
            base.OnExit(e);
        }
    }
}
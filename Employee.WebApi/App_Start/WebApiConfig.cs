using System.Reflection;
using System.Web.Http;

using Autofac;
using Autofac.Integration.WebApi;

namespace Employee.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Autofac configuration:
            // http://docs.autofac.org/en/latest/integration/webapi.html#per-controller-type-services

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(Assembly.Load("Employee.DataAccess"));
            builder.RegisterAssemblyModules(Assembly.Load("Employee.BusinessLogic"));

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
           
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;

        }
    }
}

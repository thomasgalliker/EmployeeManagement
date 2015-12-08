using System.Reflection;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

using Autofac;
using Autofac.Integration.WebApi;

using Employee.WebApi.ExceptionHandling;

namespace Employee.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Services.Replace(typeof(IExceptionHandler), new CustomExceptionHandler());
            config.Services.Add(typeof(IExceptionLogger), new TraceExceptionLogger());

            config.Filters.Add(new ControllerExceptionHandler());

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
            builder.RegisterAssemblyModules(Assembly.Load("Employee.Mapping"));

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;

            // In order to avoid recursive json serialization:
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}

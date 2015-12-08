using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Employee.WebApi.Controllers;

namespace Employee.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            TaskScheduler.UnobservedTaskException += (sender, e) =>
                {
                    e.SetObserved();
                    Trace.TraceError(e.Exception.ToString());
                };
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = this.Server.GetLastError();
            this.Server.ClearError();

            int statusCode = 0;

            if (lastError.GetType() == typeof(HttpException))
            {
                statusCode = ((HttpException)lastError).GetHttpCode();
            }
            else
            {
                // Not an HTTP related error so this is a problem in our code, set status to
                // 500 (internal server error)
                statusCode = 500;
            }

            HttpContextWrapper contextWrapper = new HttpContextWrapper(this.Context);

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("statusCode", statusCode);
            routeData.Values.Add("exception", lastError);
            routeData.Values.Add("isAjaxRequet", contextWrapper.Request.IsAjaxRequest());

            IController controller = new ErrorController();
            var requestContext = new RequestContext(contextWrapper, routeData);
            controller.Execute(requestContext);

            this.Response.End();
        }
    }
}
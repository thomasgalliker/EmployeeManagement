using System.Diagnostics;
using System.Web.Http.ExceptionHandling;

namespace Employee.WebApi.ExceptionHandling
{
    public class TraceExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            Trace.TraceError(context.ExceptionContext.Exception.ToString());
        }
    }
}
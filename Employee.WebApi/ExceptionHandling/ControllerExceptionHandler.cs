using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Employee.WebApi.ExceptionHandling
{
    public class ControllerExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            string content = "Oops! Sorry! Something went wrong." +
            "Please contact support@contoso.com so we can try to fix it.";

            if (context.Exception is CustomException)
            {
                content = context.Exception.Message;
            }

            context.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent(content)
            };
        }
    }
}
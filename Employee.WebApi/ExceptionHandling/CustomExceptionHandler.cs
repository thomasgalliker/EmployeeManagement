using System.Web.Http.ExceptionHandling;

namespace Employee.WebApi.ExceptionHandling
{
    public class CustomExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            string errorMessage = "Oops! Sorry! Something went wrong." +
                                  "Please contact support@contoso.com so we can try to fix it.";

            if (context.Exception is CustomException)
            {
                errorMessage = context.Exception.Message;
            }

            context.Result = new TextPlainErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = errorMessage
            };
        }
    }
}
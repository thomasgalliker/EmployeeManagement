using System;
using System.Web.Mvc;

using Employee.WebApi.Models;

namespace Employee.WebApi.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(int statusCode, Exception exception, bool isAjaxRequet)
        {
            this.Response.StatusCode = statusCode;

            // If it's not an AJAX request that triggered this action then just return the error view
            if (!isAjaxRequet)
            {
                var errorModel = new ErrorModel
                {
                    HttpStatusCode = statusCode,
                    Exception = exception
                };
                return this.View(errorModel);
            }
            else
            {
                // Otherwise, if it was an AJAX request, return an anon type with the message from the exception
                return this.Json(exception.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
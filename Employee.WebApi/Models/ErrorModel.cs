using System;

namespace Employee.WebApi.Models
{
    public class ErrorModel // TODO GATH: Consolidate with TextPlainErrorResult
    {
        public int HttpStatusCode { get; set; }

        public Exception Exception { get; set; }
    }
}
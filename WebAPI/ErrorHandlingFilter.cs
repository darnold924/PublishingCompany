using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace WebAPI
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        private readonly IConfiguration _configuration;

        public ErrorHandlingFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void OnException(ExceptionContext context)
        {
            var log = new LoggerConfiguration().ReadFrom.Configuration(_configuration).CreateLogger();

            var exception = context.Exception;

            if (exception.InnerException != null)
            {
                log.Error("Message = " + exception.Message + " Inner.Exception.Message = " +
                          exception.InnerException.Message
                          + " Stack Trace = " + exception.StackTrace);
            }
            else
            {
                log.Error("Message = " + exception.Message + " Stack Trace = " + exception.StackTrace);
            }

            context.ExceptionHandled = false; //optional 
        }
    }
}

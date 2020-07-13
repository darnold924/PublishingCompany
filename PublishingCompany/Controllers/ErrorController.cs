using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PublishingCompany.Models;

namespace PublishingCompany.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
                
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            // Get which route the exception occurred at

            string routeWhereExceptionOccurred = exceptionFeature.Path;

            // Get the exception that occurred

            var requestid = "";

            requestid = Activity.Current?.Id != null ? Activity.Current?.Id : HttpContext.TraceIdentifier;

            Exception exceptionThatOccurred = exceptionFeature.Error;

            _logger.LogError("Request Id: " + requestid + " " + routeWhereExceptionOccurred + " " + exceptionThatOccurred);

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
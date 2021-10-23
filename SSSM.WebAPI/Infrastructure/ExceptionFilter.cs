using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace SSSM.WebAPI
{
    /// <summary>
    /// Global exception filter for unhandled exceptions
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var endpoint = context.HttpContext.Request.Path;
            _logger.LogError(context.Exception, $"Unhandled Exception => endpoint: {endpoint}");
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Net;

namespace HawkMiddlewares
{
    public class GlobalExceptionMiddleware
    {
        //constructor and service injection

        private readonly RequestDelegate _next;
        private readonly GlobalExceptionOptions _config;
        private readonly ILogger _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, IOptions<GlobalExceptionOptions> config, ILoggerFactory logger)
        {
            _next = next;
            _config = config.Value;
            _logger = logger.CreateLogger(typeof(GlobalExceptionMiddleware));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger?.LogError("Unhandled exception", ex);

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(new ErrorDetail()
            {
                ErrorId = Guid.NewGuid().ToString(),
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error.",
                Date = DateTime.UtcNow
            }.ToString());
        }

        //additional methods
    }
}



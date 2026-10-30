using Azure;
using System.Text.Json;

namespace SalesLeadManagement.Middleware
{
    public class GlobalErrorMiddleware
    {
        private readonly ILogger<GlobalErrorMiddleware> _logger;
        private readonly RequestDelegate _next;
        public GlobalErrorMiddleware(RequestDelegate next,ILogger<GlobalErrorMiddleware> logger) {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await _next(context);
            }
            catch (Exception ex)
            {
              _logger.LogError(ex, "Unexpected exception occurred. Path: {Path}",context.Request.Path);

                if (context.Response.HasStarted)
                {
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var res = new
                {
                    statusCode = 500,
                    message = "An unexpected error occurred."
                };

                await context.Response.WriteAsJsonAsync(res);
            }
        }
    }
}

using LogCorner.EduSync.Speech.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace LogCorner.EduSync.Speech.Presentation.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _loggerFactory;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var logger = _loggerFactory.CreateLogger("ExceptionMiddleware");
#pragma warning disable CA2254 // Template should be a static expression
                logger.LogError($"Something went wrong: {ex.Message}");
                logger.LogError(ex.StackTrace);
                await HandleExceptionAsync(ex, httpContext);
            }
        }

        private static Task HandleExceptionAsync(Exception ex, HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var errorCode = (int)HttpStatusCode.InternalServerError;
            var message = "Internal Server Error.";
            if (ex is BaseException exception)
            {
                errorCode = exception.ErrorCode;
                message = exception.Message;
            }

            return context.Response.WriteAsync(JsonSerializer.Serialize(
                new
                {
                    errorCode,
                    message
                }));
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Threading.Tasks;

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
            catch (PresentationException ex)
            {
                var logger = _loggerFactory.CreateLogger("ExceptionMiddleware");
                logger.LogError($"Something went wrong: {ex.StackTrace}");
                await HandleExceptionAsync(httpContext);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(
            new
            {
                context.Response.StatusCode,
                Message = "Internal Server Error."
            }.ToString());
        }
    }
}
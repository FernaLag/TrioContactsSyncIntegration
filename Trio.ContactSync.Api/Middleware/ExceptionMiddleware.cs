using Newtonsoft.Json;
using System.Net;
using Trio.ContactSync.Application.Exceptions;

namespace Trio.ContactSync.Api.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            HttpStatusCode httpStatusCode = exception switch
            {
                BadRequestException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError,
            };

            string errorDetails = JsonConvert.SerializeObject(new ErrorDetails
            {
                ErrorType = ErrorType.Failure,
                ErrorMessage = exception.Message
            });

            httpContext.Response.StatusCode = (int)httpStatusCode;
            httpContext.Response.ContentType = "application/json";

            return httpContext.Response.WriteAsync(errorDetails);
        }
    }

    public class ErrorDetails
    {
        public ErrorType ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }

    public enum ErrorType
    {
        Failure,
        ValidationError,
        NotFound
    }
}

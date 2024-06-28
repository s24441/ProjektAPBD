using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using System.Security;

namespace ProjektAPBD.WebApi.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (SecurityException ex)
            {
                _logger.LogError(ex, "An security exception occured");
                await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized);
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogError(ex, "An security exception occured");
                await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occured");
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                Error = new
                {
                    Message = "An error occured while processing your request",
                    Detail = exception.Message
                }
            };

            var json = JsonConvert.SerializeObject(response);

            return context.Response.WriteAsync(json);
        }
    }
}

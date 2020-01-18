using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Application.Exceptions;

public class ApiErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiErrorHandlerMiddleware> _logger;

    public ApiErrorHandlerMiddleware(RequestDelegate next, ILogger<ApiErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var errorCode = "SERVER_ERROR";

        if (exception is AccessDeniedException)
        {
            code = HttpStatusCode.Unauthorized;
            errorCode = "ACCESS_DENIED";

            _logger.LogInformation(exception, exception.Message);
        }
        else if (exception is AccessForbiddenException)
        {
            code = HttpStatusCode.Forbidden;
            errorCode = "ACCESS_FORBIDDEN";

            _logger.LogInformation(exception, exception.Message);
        }
        else if (exception is DuplicateResultException)
        {
            code = HttpStatusCode.Conflict;
            errorCode = "REQUEST_CONFLICT";

            _logger.LogInformation(exception, exception.Message);
        }
        else
        {
            _logger.LogError(exception, exception.Message);
        }

        var result = JsonConvert.SerializeObject(new { errorCode });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}
public static class ErrorHandlerMiddlewareExtentions
{
    public static void UseApiErrorHandlerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ApiErrorHandlerMiddleware>();
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected

        //if (exception is MyNotFoundException) code = HttpStatusCode.NotFound;
        //else if (ex is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
        //else if (ex is MyException) code = HttpStatusCode.BadRequest;

        _logger.LogError(exception, exception.Message);

        var result = JsonConvert.SerializeObject(new { Code = "SERVER_ERROR", Error = exception.Message });
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

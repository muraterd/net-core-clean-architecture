using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Application.Exceptions;
using FluentValidation;
using System.Linq;

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

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // var contentType = context.Request.Headers["Content-Type"].ToString() ?? "";

        // if (contentType != "application/json")
        // {
        //     _logger.LogError(exception, exception.Message);
        //     await _next(context);
        //     return;
        // }

        // var code = HttpStatusCode.InternalServerError;
        // var errorCode = "SERVER_ERROR";
        // var result = "";

        // switch (exception)
        // {
        //     case AccessDeniedException ex:
        //         code = HttpStatusCode.Unauthorized;
        //         errorCode = "ACCESS_DENIED";

        //         _logger.LogInformation(exception, exception.Message);
        //         break;
        //     case AccessForbiddenException ex:
        //         code = HttpStatusCode.Forbidden;
        //         errorCode = "ACCESS_FORBIDDEN";

        //         _logger.LogInformation(exception, exception.Message);
        //         break;
        //     case DuplicateResultException ex:
        //         code = HttpStatusCode.Conflict;
        //         errorCode = "REQUEST_CONFLICT";

        //         _logger.LogInformation(exception, exception.Message);
        //         break;
        //     case ValidationException ex:
        //         code = HttpStatusCode.UnprocessableEntity;

        //         var errors = ex.Errors.Select(error => new { Field = error.PropertyName, Message = error.ErrorMessage });
        //         result = JsonConvert.SerializeObject(new { errorCode = "VALIDATION_FAILED", errors });

        //         _logger.LogInformation(exception, exception.Message);
        //         break;
        //     default:
        //         _logger.LogError(exception, exception.Message);
        //         break;
        // }

        // context.Response.ContentType = "application/json";
        // context.Response.StatusCode = (int)code;

        // if (string.IsNullOrEmpty(result))
        // {
        //     result = JsonConvert.SerializeObject(new { errorCode });
        // }
        // await context.Response.WriteAsync(result);
    }
}
public static class ErrorHandlerMiddlewareExtentions
{
    public static void UseApiErrorHandlerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ApiErrorHandlerMiddleware>();
    }
}

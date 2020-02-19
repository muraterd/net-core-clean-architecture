using System.Collections.Generic;
using System.Linq;
using Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<ApiExceptionFilterAttribute>)) as ILogger<ApiExceptionFilterAttribute>;

        var response = new { ErrorCode = "" };

        switch (context.Exception)
        {
            case AccessDeniedException exception:
                context.Result = new UnauthorizedObjectResult(new ApiErrorResponse("ACCESS_DENIED"));
                logger.LogInformation(exception, exception.Message);
                return;
            case AccessForbiddenException exception:
                context.HttpContext.Response.StatusCode = 403;
                context.Result = new JsonResult(new ApiErrorResponse("ACCESS_FORBIDDEN"));
                logger.LogInformation(exception, exception.Message);
                return;
            case DuplicateResultException exception:
                context.Result = new ConflictObjectResult(new ApiErrorResponse("REQUEST_CONFLICT"));
                logger.LogInformation(exception, exception.Message);
                return;
            case ValidationException exception:
                context.HttpContext.Response.StatusCode = 422;
                // var errors = exception.Errors.Select(error => new ValidationError(error.PropertyName, error.ErrorMessage));
                // context.Result = new JsonResult(new ApiValidationErrorResponse("VALIDATION_FAILED", errors));

                var errors = exception.Errors.Select(error => new { Field = error.PropertyName, Message = error.ErrorMessage });
                context.Result = new JsonResult(new { errorCode = "VALIDATION_FAILED", errors });
                logger.LogInformation(exception, exception.Message);
                return;
            default:
                context.HttpContext.Response.StatusCode = 500;
                context.Result = new JsonResult(new ApiErrorResponse("SERVER_ERROR"));
                logger.LogError(context.Exception, context.Exception.Message);
                return;
        }
    }
}

public class ApiErrorResponse
{
    public string ErrorCode { get; set; }

    public ApiErrorResponse(string ErrorCode)
    {
        this.ErrorCode = ErrorCode;
    }
}

public class ApiValidationErrorResponse : ApiErrorResponse
{
    public IEnumerable<ValidationError> errors { get; set; }

    public ApiValidationErrorResponse(string ErrorCode, IEnumerable<ValidationError> errors) : base(ErrorCode)
    {

    }
}

public class ValidationError
{
    public string Field { get; set; }
    public string Message { get; set; }

    public ValidationError(string Field, string Message)
    {
        this.Field = Field;
        this.Message = Message;
    }
}
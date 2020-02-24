using System.Collections.Generic;
using System.Linq;
using Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class LogExceptionsAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<LogExceptionsAttribute>)) as ILogger<LogExceptionsAttribute>;

        logger.LogError(context.Exception, context.Exception.Message);
    }
}
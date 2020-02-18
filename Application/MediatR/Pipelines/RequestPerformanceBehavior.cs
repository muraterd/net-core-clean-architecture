using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class RequestPerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly Stopwatch timer;
    private readonly ILogger logger;

    public RequestPerformanceBehavior(ILogger<TRequest> logger)
    {
        timer = new Stopwatch();
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        timer.Start();

        var response = await next();

        timer.Stop();

        var elapsedMilliseconds = timer.ElapsedMilliseconds;

        if(elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            //var userId
            //var username

            logger.LogWarning("Long Running Request {Name} (elapsed {ElapsedMilliseconds} ms) {@Request}", requestName, elapsedMilliseconds, request);
            //logger.LogInformation("Request {Name} {@userId} {@userName} {@Request}", requestName, userId);
        }

        return response;
    }
}
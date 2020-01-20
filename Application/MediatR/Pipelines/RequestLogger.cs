using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly ILogger logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RequestLogger(ILogger<TRequest> logger, IHttpContextAccessor _httpContextAccessor)
    {
        this.logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        //var userId
        //var username

        logger.LogInformation("Request {Name} {@Request}", requestName, request);
        //logger.LogInformation("Request {Name} {@userId} {@userName} {@Request}", requestName, userId);
    }
}
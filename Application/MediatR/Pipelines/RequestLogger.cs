using Data;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly ILogger logger;
    private readonly CurrentUser currentUser;

    public RequestLogger(ILogger<TRequest> logger, CurrentUser currentUser)
    {
        this.logger = logger;
        this.currentUser = currentUser;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Request {Name} {@userId} {@email} {@Request}", requestName, currentUser.Id, currentUser.Email, request);
    }
}
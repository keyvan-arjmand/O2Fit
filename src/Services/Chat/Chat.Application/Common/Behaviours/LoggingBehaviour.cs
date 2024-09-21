namespace Chat.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        //var userId = _currentUserService.UserId ?? string.Empty;
        //string? userName = string.Empty;

        //if (!string.IsNullOrEmpty(userId))
        //{
        //Todo get username from claims
        //userName = await _identityService.GetUserNameAsync(userId);
        //}

        _logger.LogInformation("Chat Request: {Name}  {@Request}",
            requestName, request);

        return Task.CompletedTask;
    }
}

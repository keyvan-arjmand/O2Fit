namespace Notification.Application.Jobs;

public class RemoveMessageLogAfterSevenDaysJob : IJob
{
    private readonly IMediator _mediator;

    public RemoveMessageLogAfterSevenDaysJob(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messageLogs = await _mediator.Send(new GetAllMessageLogAfterSevenDaysQuery());
        if (messageLogs.Any())
        {
            foreach (var messageLog in messageLogs)
            {
                await _mediator.Send(new RemoveMessageLogCommand(messageLog.Id));
            }
        }        
    }
}
using Chat.Application.Groups.V1.Commands.DeleteGroup;
using Chat.Application.Groups.V1.Queries.GetAllGroups;

namespace Chat.Application.Jobs;

public class DeleteGroupsAfterThreeMonthsJob : IJob
{
    private readonly IMediator _mediator;

    public DeleteGroupsAfterThreeMonthsJob(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var allGroups = await _mediator.Send(new GetAllGroupsQuery());
        foreach (var group in allGroups.Where(x=> (DateTime.UtcNow - x.CreatedDate).TotalDays >= 90))
        {
            await _mediator.Send(new DeleteGroupCommand(group.Id));
        }
    }
}
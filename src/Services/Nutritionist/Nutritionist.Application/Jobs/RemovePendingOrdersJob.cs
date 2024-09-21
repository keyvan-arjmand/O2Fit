namespace Nutritionist.Application.Jobs;

public class RemovePendingOrdersJob : IJob
{
    private readonly IMediator _mediator;

    public RemovePendingOrdersJob(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var allPendingOrders = await _mediator.Send(new GetAllNutritionistPendingOrdersQuery());
        foreach (var nutritionistOrderDto in allPendingOrders.Where(nutritionistOrderDto => (DateTime.UtcNow - nutritionistOrderDto.Created).TotalHours >= 48))
        {
            await _mediator.Send(new RejectNutritionistOrderCommand(nutritionistOrderDto.Id));
        }
    }
}
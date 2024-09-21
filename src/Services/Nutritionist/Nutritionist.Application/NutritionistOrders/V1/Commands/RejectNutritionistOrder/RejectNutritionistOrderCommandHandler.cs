namespace Nutritionist.Application.NutritionistOrders.V1.Commands.RejectNutritionistOrder;

public class RejectNutritionistOrderCommandHandler : IRequestHandler<RejectNutritionistOrderCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly ICurrentUserService _currentUserService;
    public RejectNutritionistOrderCommandHandler(IUnitOfWork uow, ICurrentUserService currentUserService)
    {
        _uow = uow;
        _currentUserService = currentUserService;
    }

    public async Task Handle(RejectNutritionistOrderCommand request, CancellationToken cancellationToken)
    {
        var nutritionistOrder =
            await _uow.GenericRepository<NutritionistOrder>().GetByIdAsync(request.Id, cancellationToken);

        if (nutritionistOrder == null)
            throw new NotFoundException(nameof(NutritionistOrder), request.Id);

        nutritionistOrder.Status = OrderStatus.Rejected;

        var @event = new OrderRejected
        {
            OrderId = nutritionistOrder.OrderId.ToString(),
            UserId = nutritionistOrder.NutritionistId.ToString(),
            Username = _currentUserService.Username
        };
        
        nutritionistOrder.AddDomainEvent(@event);
         
        await _uow.GenericRepository<NutritionistOrder>().UpdateOneAsync(x => x.Id == request.Id, nutritionistOrder,
            new Expression<Func<NutritionistOrder, object>>[]
            {
                x => x.Status
            }, null,cancellationToken);
    }
}
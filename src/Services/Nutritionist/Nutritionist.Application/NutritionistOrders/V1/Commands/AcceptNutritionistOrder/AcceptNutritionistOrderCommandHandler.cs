namespace Nutritionist.Application.NutritionistOrders.V1.Commands.AcceptNutritionistOrder;

public class AcceptNutritionistOrderCommandHandler : IRequestHandler<AcceptNutritionistOrderCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly ICurrentUserService _currentUserService;

    public AcceptNutritionistOrderCommandHandler(IUnitOfWork uow, ICurrentUserService currentUserService)
    {
        _uow = uow;
        _currentUserService = currentUserService;
    }

    public async Task Handle(AcceptNutritionistOrderCommand request, CancellationToken cancellationToken)
    {
        var nutritionistOrder =
            await _uow.GenericRepository<NutritionistOrder>().GetByIdAsync(request.Id, cancellationToken);
        
        if (nutritionistOrder == null)
            throw new NotFoundException(nameof(NutritionistOrder), request.Id);

        nutritionistOrder.Status = OrderStatus.Accepted;

        var orderAcceptedEvent = new OrderAccepted
        {
            OrderId = nutritionistOrder.OrderId.ToString(),
            UserId = nutritionistOrder.NutritionistId.ToString(),
            Username = _currentUserService.Username
        };
        nutritionistOrder.AddDomainEvent(orderAcceptedEvent);
        
        await _uow.GenericRepository<NutritionistOrder>().UpdateOneAsync(x => x.Id == request.Id, nutritionistOrder,
            new Expression<Func<NutritionistOrder, object>>[]
            {
                x => x.Status
            }, null,cancellationToken);
    }
}
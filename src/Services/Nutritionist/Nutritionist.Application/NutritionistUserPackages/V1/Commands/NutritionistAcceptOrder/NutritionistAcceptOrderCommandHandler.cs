namespace Nutritionist.Application.NutritionistUserPackages.V1.Commands.NutritionistAcceptOrder;

public class NutritionistAcceptOrderCommandHandler : IRequestHandler<NutritionistAcceptOrderCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly ICurrentUserService _currentUserService;
    public NutritionistAcceptOrderCommandHandler(IUnitOfWork uow, ICurrentUserService currentUserService)
    {
        _uow = uow;
        _currentUserService = currentUserService;
    }

    public async Task Handle(NutritionistAcceptOrderCommand request, CancellationToken cancellationToken)
    {
        var nutritionistUserPackage = new NutritionistUserPackage(request.OrderId, request.OrderStatus, request.UserId);

        AddDomainEvent(request, nutritionistUserPackage);

        await _uow.GenericRepository<NutritionistUserPackage>()
            .InsertOneAsync(nutritionistUserPackage, null, cancellationToken);
    }

    private void AddDomainEvent(NutritionistAcceptOrderCommand request, NutritionistUserPackage nutritionistUserPackage)
    {
        var @event = new OrderAccepted
        {
            OrderId = request.OrderId,
            Username = _currentUserService.Username!,
            UserId = _currentUserService.UserId!
        };
        nutritionistUserPackage.AddDomainEvent(@event);
    }
}
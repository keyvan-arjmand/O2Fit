namespace Nutritionist.Application.NutritionistOrders.V1.Commands.CreateNutritionistOrder;

public class CreateNutritionistOrderCommandHandler : IRequestHandler<CreateNutritionistOrderCommand>
{
    private readonly IUnitOfWork _uow;

    public CreateNutritionistOrderCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(CreateNutritionistOrderCommand request, CancellationToken cancellationToken)
    {
        var nutritionistOrder = new NutritionistOrder(ObjectId.Parse(request.UserId), request.Username,
            request.ProfileImageName,
            ObjectId.Parse(request.NutritionistId), ObjectId.Parse(request.PackageId), ObjectId.Parse(request.OrderId),
            OrderStatus.Pending,
            new DietCategoryTranslation(request.DietCategoryPersian, request.DietCategoryEnglish,
                request.DietCategoryArabic),
            new PackageTranslation(request.PackageNamePersian, request.PackageNameEnglish, request.PackageNameArabic));

        nutritionistOrder.CreatedById = ObjectId.Parse(request.UserId);
        AddDomainEvent(request, nutritionistOrder);

        await _uow.GenericRepository<NutritionistOrder>()
            .InsertOneManualAuditLogAsync(nutritionistOrder, null, cancellationToken);
    }

    private void AddDomainEvent(CreateNutritionistOrderCommand request, NutritionistOrder nutritionistOrder)
    {
        var @event = new NutritionistOrderCreated
        {
            NutritionistId = request.NutritionistId,
            PackageNameArabic = request.PackageNameArabic,
            PackageNameEnglish = request.PackageNameEnglish,
            PackageNamePersian = request.PackageNamePersian
        };

        nutritionistOrder.AddDomainEvent(@event);
    }
}
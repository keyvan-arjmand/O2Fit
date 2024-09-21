using Common.Enums.TypeEnums;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Domain.Aggregates.DiscountNutritionistAggregate;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;
using Discount.Domain.ValueObjects;

namespace Discount.Application.DiscountNutritionists.V1.Commands.GenerateDiscountNutritionist;

public class GenerateDiscountNutritionistCommandHandler:IRequestHandler<GenerateDiscountNutritionistCommand>
{
    private readonly IUnitOfWork _uow;

    public GenerateDiscountNutritionistCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(GenerateDiscountNutritionistCommand request, CancellationToken cancellationToken)
    {
        var discount = new DiscountNutritionist
        {
            Code = new DiscountCode(await _uow.DiscountNutritionistRepository().GenerationCode()),
            StartDate = request.StartDate,
            EndDateTime = request.EndDateTime,
            IsActive = request.IsActive,
            Description = new DiscountNutritionistTranslation
            {
                English = request.Description.English,
                Persian = request.Description.Persian,
                Arabic = request.Description.Arabic
            },
            UsableCount = request.UsableCount,
            DiscountType = !string.IsNullOrEmpty(request.UserId)
                ? DiscountType.DiscountCodePerUser
                : DiscountType.DiscountCodeGeneral,
            PackageType = PackageType.NutritionistDietPack,
            CountryId = request.CountryIds,
            Amount = request.Amount,
            UserId = !string.IsNullOrEmpty(request.UserId)
                ? request.UserId.StringToObjectId()
                : ObjectId.Empty,
        };
        await _uow.GenericRepository<DiscountNutritionist>()
            .InsertOneAsync(discount, null, cancellationToken);
    }
}
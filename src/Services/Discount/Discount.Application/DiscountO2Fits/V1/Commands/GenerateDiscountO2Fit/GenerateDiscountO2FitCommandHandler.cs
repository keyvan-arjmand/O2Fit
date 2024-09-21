using Common.Enums.TypeEnums;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;
using Discount.Domain.ValueObjects;

namespace Discount.Application.DiscountO2Fits.V1.Commands.GenerateDiscountO2Fit;

public class GenerateDiscountO2FitCommandHandler:IRequestHandler<GenerateDiscountO2FitCommand>
{
    private readonly IUnitOfWork _uow;

    public GenerateDiscountO2FitCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(GenerateDiscountO2FitCommand request, CancellationToken cancellationToken)
    {
        var discount = new DiscountO2Fit
        {
            Code = new DiscountCode(await _uow.DiscountRepository().GenerationCode()),
            StartDate = request.StartDate,
            EndDateTime = request.EndDateTime,
            IsActive = request.IsActive,
            Description = new DiscountO2FitTranslation
            {
                English = request.Description.English,
                Persian = request.Description.Persian,
                Arabic = request.Description.Arabic
            },
            UsableCount = request.UsableCount,
            DiscountType = !string.IsNullOrEmpty(request.UserId)
                ? DiscountType.DiscountCodePerUser
                : DiscountType.DiscountCodeGeneral,
            PackageType = request.PackageType,
            CountryId = request.CountryIds,
            Percent = request.Percent,
            UserId = !string.IsNullOrEmpty(request.UserId)
                ? request.UserId.StringToObjectId()
                : ObjectId.Empty,
        };
        await _uow.GenericRepository<DiscountO2Fit>()
            .InsertOneAsync(discount, null, cancellationToken);
    }
}
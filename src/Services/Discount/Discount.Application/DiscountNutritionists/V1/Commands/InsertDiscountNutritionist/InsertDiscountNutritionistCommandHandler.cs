using Common.Enums.TypeEnums;
using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.DiscountO2Fits.V1.Commands.InsertDiscountO2Fit;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;
using Discount.Domain.ValueObjects;

namespace Discount.Application.DiscountNutritionists.V1.Commands.InsertDiscountNutritionist;

public class InsertDiscountNutritionistCommandHandler : IRequestHandler<InsertDiscountO2FitCommand>
{
    private readonly IUnitOfWork _uow;

    public InsertDiscountNutritionistCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(InsertDiscountO2FitCommand request, CancellationToken cancellationToken)
    {
        var filterDiscount =
            Builders<DiscountO2Fit>.Filter.Eq(x => x.Code,
                new DiscountCode(request.Code));
        var discountExist = await _uow.GenericRepository<DiscountO2Fit>()
            .GetSingleDocumentByFilterAsync(filterDiscount, cancellationToken);
        if (discountExist != null)
            throw new AppException(ApiResultStatusCode.BadRequest, "Discount Code Already Exist");
        var discount = new DiscountO2Fit
        {
            Code = new DiscountCode(request.Code),
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
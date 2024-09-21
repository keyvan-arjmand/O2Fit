using Common.Enums.TypeEnums;
using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Currencies.V1.Queries.ExChangeCurrency;
using Discount.Application.Dtos;
using Discount.Domain.Aggregates.DiscountNutritionistAggregate;
using Discount.Domain.Aggregates.DiscountPackageNutritionistAggregate;
using Discount.Domain.Aggregates.DiscountPackageO2FitAggregate;
using Discount.Domain.ValueObjects;
using EventBus.Messages.Contracts.Services.Payments.Package;
using MassTransit;

namespace Discount.Application.DiscountNutritionists.V1.Commands.ApplyDiscountCodeNutritionist;

public class
    ApplyDiscountCodeNutritionistCommandHandler : IRequestHandler<ApplyDiscountCodeNutritionistCommand,
        ApplyDiscountCodeNutritionistResult>
{
    private readonly IUnitOfWork _work;
    private readonly IRequestClient<GetByIdPackage> _client;
    private readonly IMediator _mediator;

    public ApplyDiscountCodeNutritionistCommandHandler(IUnitOfWork work, IRequestClient<GetByIdPackage> client,
        IMediator mediator)
    {
        _work = work;
        _client = client;
        _mediator = mediator;
    }

    public async Task<ApplyDiscountCodeNutritionistResult> Handle(ApplyDiscountCodeNutritionistCommand request,
        CancellationToken cancellationToken)
    {
        var filterCode =
            Builders<DiscountNutritionist>.Filter.Eq(x => x.Code,
                new DiscountCode(request.DiscountCode));
        var discountCode = await _work.GenericRepository<DiscountNutritionist>()
            .GetSingleDocumentByFilterAsync(filterCode, cancellationToken);

        if (discountCode == null) throw new AppException(ApiResultStatusCode.NotFound, "Discount Code Not Found");

        if (!Validation.ValidationDate(discountCode.StartDate, discountCode.EndDateTime) ||
            discountCode is { UsableCount: < 0, IsActive: false })
            throw new AppException(ApiResultStatusCode.BadRequest, "Discount Code expire");

        if (discountCode.DiscountType != DiscountType.DiscountCodeGeneral &&
            discountCode.UserId != request.UserId.StringToObjectId())
            throw new AppException(ApiResultStatusCode.BadRequest, "User cannot use this code");

        if (discountCode.DiscountType == DiscountType.DiscountCodeGeneral &&
            !discountCode.CountryId.Contains(request.UserCountryId))
            throw new AppException(ApiResultStatusCode.BadRequest, "User cannot use this code");

        //package
        var package = await _client.GetResponse<GetByIdPackageResult>(new GetByIdPackage
        {
            Id = request.PackageId
        }, cancellationToken);

        if (discountCode.PackageType != package.Message.PackageType&&
            request.UserCurrencyCode != package.Message.CurrencyCode)
            throw new AppException(ApiResultStatusCode.BadRequest, "Discount Code Not Valid For this package");

        //discount package 
        var filterPackage =
            Builders<DiscountPackageNutritionist>.Filter.Eq(x => x.PackageId,
                request.PackageId.StringToObjectId());
        var discountPackage = await _work
            .GenericRepository<DiscountPackageNutritionist>()
            .GetSingleDocumentByFilterAsync(filterPackage, cancellationToken);

        if (discountPackage != null)
        {
            if (package.Message.CurrencyCode != request.UserCurrencyCode)
            {
                return new ApplyDiscountCodeNutritionistResult
                {
                    Code = request.DiscountCode,
                    DiscountedPrice =
                        package.Message.Price.CalculateWithPercentAndAmount(discountPackage.Percent,
                            discountCode.Amount),
                    Id = package.Message.Id,
                    PricePackage = package.Message.Price,
                    PackageDiscount = package.Message.Price.CalculateWithPercent(discountPackage.Percent),
                    DiscountAmount = discountCode.Amount,
                    DiscountType = discountCode.DiscountType
                };
            }
            else
            {
                return new ApplyDiscountCodeNutritionistResult
                {
                    Code = request.DiscountCode,
                    DiscountedPrice =
                        package.Message.Price.CalculateWithPercentAndAmount(discountPackage.Percent,
                            discountCode.Amount),
                    Id = package.Message.Id,
                    PricePackage = package.Message.Price,
                    PackageDiscount = package.Message.Price.CalculateWithPercent(discountPackage.Percent),
                    DiscountAmount = discountCode.Amount,
                    DiscountType = discountCode.DiscountType
                };
            }
        }
        else
        {
            if (package.Message.CurrencyCode != request.UserCurrencyCode)
            {
             
                return new ApplyDiscountCodeNutritionistResult
                {
                    Code = request.DiscountCode,
                    DiscountedPrice =
                        package.Message.Price.CalculateWithDiscountCode(discountCode.Amount),
                    Id = package.Message.Id,
                    PricePackage = package.Message.Price,
                    PackageDiscount = package.Message.Price,
                    DiscountAmount = discountCode.Amount,
                    DiscountType = discountCode.DiscountType
                };
            }
            else
            {
                return new ApplyDiscountCodeNutritionistResult
                {
                    Code = request.DiscountCode,
                    DiscountedPrice =
                        package.Message.Price.CalculateWithDiscountCode(discountCode.Amount),
                    Id = package.Message.Id,
                    PricePackage = package.Message.Price,
                    PackageDiscount = package.Message.Price,
                    DiscountAmount = discountCode.Amount,
                    DiscountType = discountCode.DiscountType
                };
            }
        }
    }
}
using Common.Enums.TypeEnums;
using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Currencies.V1.Queries.ExChangeCurrency;
using Discount.Application.Dtos;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;
using Discount.Domain.ValueObjects;
using EventBus.Messages.Contracts.Services.Payments.Package;
using MassTransit;

namespace Discount.Application.DiscountO2Fits.V1.Commands.ApplyDiscountCodeO2Fit;

public class
    ApplyDiscountCodeO2FitCommandHandler : IRequestHandler<ApplyDiscountCodeO2FitCommand, ApplyDiscountCodeO2FitResult>
{
    private readonly IUnitOfWork _work;
    private readonly IRequestClient<GetByIdPackage> _client;
    private readonly IMediator _mediator;

    public ApplyDiscountCodeO2FitCommandHandler(IUnitOfWork work, IRequestClient<GetByIdPackage> client,
        IMediator mediator)
    {
        _work = work;
        _client = client;
        _mediator = mediator;
    }

    public async Task<ApplyDiscountCodeO2FitResult> Handle(ApplyDiscountCodeO2FitCommand request,
        CancellationToken cancellationToken)
    {
        var filterCode =
            Builders<DiscountO2Fit>.Filter.Eq(x => x.Code,
                new DiscountCode(request.DiscountCode));
        var discountCode = await _work.GenericRepository<DiscountO2Fit>()
            .GetSingleDocumentByFilterAsync(filterCode, cancellationToken);

        if (discountCode == null)
            throw new AppException(ApiResultStatusCode.NotFound, "Discount Code Not Found");

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

        if (discountCode.PackageType != package.Message.PackageType)
            throw new AppException(ApiResultStatusCode.BadRequest, "Discount Code Not Valid For this package");

        //discount package 
        var filterPackage =
            Builders<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit>.Filter.Eq(x => x.PackageId,
                request.PackageId.StringToObjectId());
        var discountPackage = await _work
            .GenericRepository<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit>()
            .GetSingleDocumentByFilterAsync(filterPackage, cancellationToken);

        if (discountPackage != null)
        {
            if (package.Message.CurrencyCode != request.UserCurrencyCode)
            {
                var exRate = await _mediator.Send(new ExChangeCurrencyQuery
                {
                    DestinationCurrencyCode = request.UserCurrencyCode,
                    SourceCurrencyAmount = package.Message.Price,
                    SourceCurrencyCode = package.Message.CurrencyCode
                }, cancellationToken);

                return new ApplyDiscountCodeO2FitResult
                {
                    Code = request.DiscountCode,
                    DiscountedPrice =
                        exRate.DestinationCurrencyAmount.CalculateWithPercent(discountPackage.Percent +
                                                                              discountCode.Percent),
                    Id = package.Message.Id,
                    PricePackage = exRate.DestinationCurrencyAmount,
                    PackageDiscount = exRate.DestinationCurrencyAmount.CalculateWithPercent(discountPackage.Percent),
                    DiscountAmount = exRate.DestinationCurrencyAmount.CalculateAmountPercent(discountCode.Percent),
                    DiscountType = discountCode.DiscountType,
                };
            }
            else
            {
                return new ApplyDiscountCodeO2FitResult
                {
                    Code = request.DiscountCode,
                    DiscountedPrice =
                        package.Message.Price.CalculateWithPercent(discountPackage.Percent + discountCode.Percent),
                    Id = package.Message.Id,
                    PricePackage = package.Message.Price,
                    PackageDiscount = package.Message.Price.CalculateWithPercent(discountPackage.Percent),
                    DiscountAmount = package.Message.Price.CalculateAmountPercent(discountCode.Percent),
                    DiscountType = discountCode.DiscountType,
                };
            }
        }
        else
        {
            if (package.Message.CurrencyCode != request.UserCurrencyCode)
            {
                var exRate = await _mediator.Send(new ExChangeCurrencyQuery
                {
                    DestinationCurrencyCode = request.UserCurrencyCode,
                    SourceCurrencyAmount = package.Message.Price,
                    SourceCurrencyCode = package.Message.CurrencyCode
                }, cancellationToken);

                return new ApplyDiscountCodeO2FitResult
                {
                    Code = request.DiscountCode,
                    DiscountedPrice =
                        exRate.DestinationCurrencyAmount.CalculateWithPercent(discountCode.Percent),
                    Id = package.Message.Id,
                    PricePackage = exRate.DestinationCurrencyAmount,
                    PackageDiscount = exRate.DestinationCurrencyAmount,
                    DiscountAmount = exRate.DestinationCurrencyAmount.CalculateAmountPercent(discountCode.Percent),
                    DiscountType = discountCode.DiscountType,
                };
            }
            else
            {
                return new ApplyDiscountCodeO2FitResult
                {
                    Code = request.DiscountCode,
                    DiscountedPrice =
                        package.Message.Price.CalculateWithPercent(discountCode.Percent),
                    Id = package.Message.Id,
                    PricePackage = package.Message.Price,
                    PackageDiscount = package.Message.Price,
                    DiscountAmount = package.Message.Price.CalculateAmountPercent(discountCode.Percent),
                    DiscountType = discountCode.DiscountType,
                };
            }
        }
    }
}
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Common.Interfaces.Services;
using Discount.Application.Currencies.V1.Queries.ExChangeCurrency;
using Discount.Application.Currencies.V1.Queries.GetCurrencyById;
using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage;
using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.O2FitPackages;
using MassTransit;

namespace Discount.Application.Consumers.DiscountPackages;

public class CalculateAmountO2FitPackageByIdConsumer : IConsumer<CalculateAmountO2FitPackageById>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public CalculateAmountO2FitPackageByIdConsumer(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CalculateAmountO2FitPackageById> context)
    {
        var filter =
            Builders<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit>.Filter.Eq(x => x.PackageId,
                context.Message.Id.StringToObjectId());
        var discountPackages = await _unitOfWork
            .GenericRepository<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit>()
            .GetSingleDocumentByFilterAsync(filter, context.CancellationToken);

        var currency = await _mediator.Send(new GetCurrencyByCodeQuery(context.Message.PackageCurrencyCode),
            context.CancellationToken);

        if (discountPackages != null)
        {
            var packages = new CalculateAmountO2FitPackageByIdResult();

            if (currency.CurrencyCode != context.Message.UserCurrencyCode)
            {
                var exRate = await _mediator.Send(new ExChangeCurrencyQuery
                {
                    DestinationCurrencyCode = context.Message.UserCurrencyCode,
                    SourceCurrencyAmount = context.Message.Price,
                    SourceCurrencyCode = currency.CurrencyCode
                }, context.CancellationToken);
                packages = new CalculateAmountO2FitPackageByIdResult
                {
                    DiscountPrice = exRate.DestinationCurrencyAmount.CalculateWithPercent(discountPackages.Percent),
                    Id = context.Message.Id,
                    Wage = exRate.DestinationCurrencyAmount.CalculateAmountPercent(discountPackages.Percent)
                };
                await context.RespondAsync(packages);
            }
            else
            {
                packages = new CalculateAmountO2FitPackageByIdResult
                {
                    DiscountPrice = context.Message.Price.CalculateWithPercent(discountPackages.Percent),
                    Id = context.Message.Id,
                    Wage = context.Message.Price.CalculateAmountPercent(discountPackages.Percent)
                };
                await context.RespondAsync(packages);
            }
        }
        else
        {
            var packages = new CalculateAmountO2FitPackageByIdResult();

            if (currency.CurrencyCode != context.Message.PackageCurrencyCode)
            {
                var exRate = await _mediator.Send(new ExChangeCurrencyQuery
                {
                    DestinationCurrencyCode = context.Message.UserCurrencyCode,
                    SourceCurrencyAmount = context.Message.Price,
                    SourceCurrencyCode = currency.CurrencyCode
                }, context.CancellationToken);
                packages = new CalculateAmountO2FitPackageByIdResult
                {
                    DiscountPrice = exRate.DestinationCurrencyAmount,
                    Id = context.Message.Id,
                    Wage = 0
                };
                await context.RespondAsync(packages);
            }
            else
            {
                packages = new CalculateAmountO2FitPackageByIdResult
                {
                    DiscountPrice = context.Message.Price,
                    Id = context.Message.Id,
                    Wage = 0
                };
                await context.RespondAsync(packages);
            }
        }
    }
}
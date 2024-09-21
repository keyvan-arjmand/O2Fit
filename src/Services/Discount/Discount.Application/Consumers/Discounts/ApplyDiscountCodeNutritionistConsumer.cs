using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.DiscountNutritionists.V1.Commands.ApplyDiscountCodeNutritionist;
using Discount.Domain.Aggregates.DiscountNutritionistAggregate;
using Discount.Domain.Aggregates.DiscountO2FitAggregate;
using Discount.Domain.ValueObjects;
using MassTransit;
using EventBus.Messages.Contracts.Services.Payments.Package;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using EventBus.Messages.Contracts.Services.Discounts.DiscountCode.NutritionistCode;

namespace Discount.Application.Consumers.Discounts;

public class ApplyDiscountCodeNutritionistConsumer : IConsumer<CalculateNutritionistDiscountCode>
{
    private readonly IMediator _mediator;

    public ApplyDiscountCodeNutritionistConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CalculateNutritionistDiscountCode> context)
    {
        var result = await _mediator.Send(new ApplyDiscountCodeNutritionistCommand(context.Message.DiscountCode,
            context.Message.PackageId, context.Message.CountryId, context.Message.UserId,
            context.Message.UserCurrencyCode), context.CancellationToken);
        if (result == null)
        {
            await context.RespondAsync<CalculateNutritionistDiscountCodeResult>(
                new CalculateNutritionistDiscountCodeResult
                {
                    Id = string.Empty,
                    Code = string.Empty,
                    DiscountedPrice = 0,
                    DiscountAmount = 0,
                    PricePackage = 0
                });
        }
        else
        {
            await context.RespondAsync<CalculateNutritionistDiscountCodeResult>(
                new CalculateNutritionistDiscountCodeResult
                {
                    Id = result.Id,
                    Code = result.Code,
                    DiscountType = result.DiscountType,
                    DiscountAmount = result.DiscountAmount,
                    DiscountedPrice =result.DiscountedPrice ,
                    PackageDiscount = result.PackageDiscount,
                    PricePackage =result.PricePackage 
                });
        }
    }
}
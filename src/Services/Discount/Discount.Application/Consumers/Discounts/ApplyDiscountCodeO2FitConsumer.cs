using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.DiscountO2Fits.V1.Commands.ApplyDiscountCodeO2Fit;
using EventBus.Messages.Contracts.Services.Discounts.DiscountCode.O2FitCode;
using MassTransit;

namespace Discount.Application.Consumers.Discounts;

public class ApplyDiscountCodeO2FitConsumer : IConsumer<CalculateO2FitDiscountCode>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public ApplyDiscountCodeO2FitConsumer(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CalculateO2FitDiscountCode> context)
    {
        var result = await _mediator.Send(new ApplyDiscountCodeO2FitCommand(context.Message.DiscountCode,
            context.Message.PackageId, context.Message.CountryId, context.Message.UserId,
            context.Message.UserCurrencyCode), context.CancellationToken);
        if (result == null)
        {
            await context.RespondAsync<CalculateO2FitDiscountCodeResult>(
                new CalculateO2FitDiscountCodeResult
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
            await context.RespondAsync<CalculateO2FitDiscountCodeResult>(
                new CalculateO2FitDiscountCodeResult
                {
                    Id = result.Id,
                    Code = result.Code,
                    DiscountType = result.DiscountType,
                    DiscountAmount = result.DiscountAmount,
                    DiscountedPrice = result.DiscountedPrice,
                    PackageDiscount = result.PackageDiscount,
                    PricePackage = result.PricePackage
                });
        }
    }
}
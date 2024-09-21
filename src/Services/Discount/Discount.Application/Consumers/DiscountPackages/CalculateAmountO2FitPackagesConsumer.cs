using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Application.Currencies.V1.Queries.ExChangeCurrency;
using EventBus.Messages.Contracts.Services.Discounts.DiscountPackage.O2FitPackages;
using MassTransit;

namespace Discount.Application.Consumers.DiscountPackages;

public class CalculateAmountO2FitPackagesConsumer : IConsumer<CalculateAmountO2FitPackages>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public CalculateAmountO2FitPackagesConsumer(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CalculateAmountO2FitPackages> context)
    {
        var packageIds = context.Message.Packages.Select(x => x.Id).ToList().StringToObjectIds();
        var filter =
            Builders<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit>.Filter.Where(x =>
                packageIds.Contains(x.PackageId));
        var discountPackages = await _unitOfWork
            .GenericRepository<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit>()
            .GetListOfDocumentsByFilterAsync(filter, context.CancellationToken);
        var result = new CalculateAmountO2FitPackagesResult();
        var packages = new List<PackageDiscount>();
        if (discountPackages.Count > 0)
        {
            foreach (var package in context.Message.Packages)
            {
                var discountPackage =
                    discountPackages.FirstOrDefault(x => x.PackageId == package.Id.StringToObjectId());
                if (package.PackageCurrencyCode != context.Message.UserCurrencyCode)
                {
                    var exRate = await _mediator.Send(new ExChangeCurrencyQuery
                    {
                        DestinationCurrencyCode = context.Message.UserCurrencyCode,
                        SourceCurrencyAmount = package.Price,
                        SourceCurrencyCode = package.PackageCurrencyCode
                    }, context.CancellationToken);
                    if (discountPackage != null)
                    {
                        packages.Add(new PackageDiscount
                        {
                            Id = package.Id,
                            DiscountPrice =
                                exRate.DestinationCurrencyAmount.CalculateWithPercent(discountPackage.Percent),
                            Wage = exRate.DestinationCurrencyAmount.CalculateAmountPercent(discountPackage.Percent),
                        });
                    }
                    else
                    {
                        packages.Add(new PackageDiscount
                        {
                            Id = package.Id,
                            DiscountPrice = exRate.DestinationCurrencyAmount,
                            Wage = 0
                        });
                    }
                }
                else
                {
                    if (discountPackage != null)
                    {
                        packages.Add(new PackageDiscount
                        {
                            Id = package.Id,
                            DiscountPrice = package.Price.CalculateWithPercent(discountPackage.Percent),
                            Wage = package.Price.CalculateAmountPercent(discountPackage.Percent)
                        });
                    }
                    else
                    {
                        packages.Add(new PackageDiscount
                        {
                            Id = package.Id,
                            DiscountPrice = package.Price,
                            Wage = 0
                        });
                    }
                }
            }

            result.Packages.AddRange(packages);
            await context.RespondAsync<CalculateAmountO2FitPackagesResult>(result);
        }
        else
        {
            foreach (var package in context.Message.Packages)
            {
                if (package.PackageCurrencyCode != context.Message.UserCurrencyCode)
                {
                    var exRate = await _mediator.Send(new ExChangeCurrencyQuery
                    {
                        DestinationCurrencyCode = context.Message.UserCurrencyCode,
                        SourceCurrencyAmount = package.Price,
                        SourceCurrencyCode = package.PackageCurrencyCode
                    }, context.CancellationToken);
                    packages.Add(new PackageDiscount
                    {
                        Id = package.Id,
                        DiscountPrice = exRate.DestinationCurrencyAmount,
                        Wage = 0
                    });
                }
                else
                {
                    packages.Add(new PackageDiscount
                    {
                        Id = package.Id,
                        DiscountPrice = package.Price,
                        Wage = 0
                    });
                }
            }

            result.Packages.AddRange(packages);
            await context.RespondAsync<CalculateAmountO2FitPackagesResult>(result);
        }
    }
}
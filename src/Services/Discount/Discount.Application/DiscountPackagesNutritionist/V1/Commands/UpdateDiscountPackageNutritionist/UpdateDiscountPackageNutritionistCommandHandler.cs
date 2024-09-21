using Common.Enums.TypeEnums;
using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using Discount.Domain.Aggregates.DiscountPackageNutritionistAggregate;
using Discount.Domain.Aggregates.DiscountPackageO2FitAggregate;
using Discount.Domain.ValueObjects;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using MassTransit;

namespace Discount.Application.DiscountPackagesNutritionist.V1.Commands.UpdateDiscountPackageNutritionist;

public class UpdateDiscountPackageNutritionistCommandHandler : IRequestHandler<UpdateDiscountPackageNutritionistCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetCurrencyByCode> _clientCurrency;

    public UpdateDiscountPackageNutritionistCommandHandler(IUnitOfWork uow,
        IRequestClient<GetCurrencyByCode> clientCurrency)
    {
        _uow = uow;
        _clientCurrency = clientCurrency;
    }

    public async Task Handle(UpdateDiscountPackageNutritionistCommand request, CancellationToken cancellationToken)
    {
        var discountPackage = await _uow.GenericRepository<DiscountPackageNutritionist>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (discountPackage == null) throw new NotFoundException("discountPackage Not Found");
        discountPackage.Percent = request.Percent;
        await _uow.GenericRepository<DiscountPackageNutritionist>()
            .UpdateOneAsync(x => x.Id == request.Id, discountPackage,
                new Expression<Func<DiscountPackageNutritionist, object>>[]
                {
                    x => x.Percent,
                }, null, cancellationToken).ConfigureAwait(false);
    }
}
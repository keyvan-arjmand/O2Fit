using Discount.Application.Common.Exceptions;
using Discount.Application.Common.Interfaces.Persistence.UoW;
using EventBus.Messages.Contracts.Services.Currencies.Currencies;
using MassTransit;

namespace Discount.Application.DiscountPackageO2Fit.V1.Commands.UpdateDiscountPackageO2Fit;

public class UpdateDiscountPackageO2FitCommandHandler : IRequestHandler<UpdateDiscountPackageO2FitCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IRequestClient<GetCurrencyByCode> _clientCurrency;
    public UpdateDiscountPackageO2FitCommandHandler(IUnitOfWork uow, IRequestClient<GetCurrencyByCode> clientCurrency)
    {
        _uow = uow;
        _clientCurrency = clientCurrency;
    }

    public async Task Handle(UpdateDiscountPackageO2FitCommand request, CancellationToken cancellationToken)
    {

        var discountPackage = await _uow.GenericRepository<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (discountPackage == null) throw new NotFoundException("discountPackage Not Found");
        discountPackage.Percent = request.Percent;
        await _uow.GenericRepository<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit>()
            .UpdateOneAsync(x => x.Id == request.Id, discountPackage, new Expression<Func<Domain.Aggregates.DiscountPackageO2FitAggregate.DiscountPackageO2Fit, object>>[]
            {
                x=>x.Percent,
            }, null, cancellationToken).ConfigureAwait(false);
    }
}
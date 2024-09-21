using Currency.Application.Common.Exceptions;
using Currency.Application.Common.Interfaces.Persistence.UoW;
using Currency.Application.Common.Interfaces.Services;

namespace Currency.Application.Currencies.V1.Commands.DeleteCurrency;

public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IResponseCacheService _cacheService;


    public DeleteCurrencyCommandHandler(IUnitOfWork uow, IResponseCacheService cacheService)
    {
        _uow = uow;
        _cacheService = cacheService;
    }


    public async Task Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currency = await _uow.GenericRepository<Domain.Aggregates.CurrencyAggregate.Currency>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (currency == null) throw new NotFoundException("currency Not Found");
        currency.IsDelete = true;
        await _cacheService.DeleteKeyAsync(currency.CurrencyCode);
        await _uow.GenericRepository<Domain.Aggregates.CurrencyAggregate.Currency>()
            .SoftDeleteByIdAsync(request.Id, currency, null, cancellationToken).ConfigureAwait(false);
    }
}
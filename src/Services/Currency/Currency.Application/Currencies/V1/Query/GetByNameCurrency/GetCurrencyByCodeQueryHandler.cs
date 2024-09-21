using Currency.Application.Common.Exceptions;
using Currency.Application.Common.Interfaces.Persistence.UoW;
using Currency.Application.Common.Mapping;
using Currency.Application.Dtos;
using Currency.Domain.ValueObjects;

namespace Currency.Application.Currencies.V1.Query.GetByNameCurrency;

public class GetCurrencyByCodeQueryHandler : IRequestHandler<GetCurrencyByCodeQuery, CurrencyDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetCurrencyByCodeQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<CurrencyDto> Handle(GetCurrencyByCodeQuery request, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.CurrencyAggregate.Currency>.Filter.Eq(x => x.CurrencyCode,new CurrencyCode( request.CurrencyCode));
        var currency = await _uow.GenericRepository<Domain.Aggregates.CurrencyAggregate.Currency>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        if (currency == null) throw new NotFoundException("currency Not Found");
        return _mapper.Map<Domain.Aggregates.CurrencyAggregate.Currency, CurrencyDto>(currency);
    }
}
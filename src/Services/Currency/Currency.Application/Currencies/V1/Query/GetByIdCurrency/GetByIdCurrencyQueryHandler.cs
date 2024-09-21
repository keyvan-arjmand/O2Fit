using Currency.Application.Common.Exceptions;
using Currency.Application.Common.Interfaces.Persistence.UoW;
using Currency.Application.Common.Mapping;
using Currency.Application.Dtos;
using Currency.Domain.ValueObjects;

namespace Currency.Application.Currencies.V1.Query.GetByIdCurrency;

public class GetByIdCurrencyQueryHandler : IRequestHandler<GetByIdCurrencyQuery, CurrencyDto>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetByIdCurrencyQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task<CurrencyDto> Handle(GetByIdCurrencyQuery request, CancellationToken cancellationToken)
    {
        var currency = await _uow.GenericRepository<Domain.Aggregates.CurrencyAggregate.Currency>()
            .GetByIdAsync(request.Id,cancellationToken);
        if (currency == null) throw new NotFoundException("currency Not Found");
        return _mapper.Map<Domain.Aggregates.CurrencyAggregate.Currency, CurrencyDto>(currency);
    }
}
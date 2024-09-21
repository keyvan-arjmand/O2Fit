using Currency.Application.Common.Interfaces.Persistence.UoW;
using Currency.Application.Common.Mapping;
using Currency.Application.Dtos;
using Currency.Domain.ValueObjects;
using System.Collections.Generic;

namespace Currency.Application.Currencies.V1.Query.GetAllCurrency;

public class GetAllCurrencyQueryHandler : IRequestHandler<GetAllCurrencyQuery, List<CurrencyDto>>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetAllCurrencyQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<List<CurrencyDto>> Handle(GetAllCurrencyQuery request, CancellationToken cancellationToken)
    {
        var currency = await _uow.GenericRepository<Domain.Aggregates.CurrencyAggregate.Currency>()
            .GetAllAsync(cancellationToken);
        return currency.ToDto<CurrencyDto>().ToList();
    }
}
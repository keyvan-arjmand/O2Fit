using Market.Application.Common.Exceptions;
using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.MarketMessageAggregate;

namespace Market.Application.MarketMessages.V1.Queries.GetByIdMarketMessage;

public class GetByIdMarketMessageQueryHandler : IRequestHandler<GetByIdMarketMessageQuery, MarketMessageDto>
{
    private readonly IUnitOfWork _work;

    public GetByIdMarketMessageQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<MarketMessageDto> Handle(GetByIdMarketMessageQuery request, CancellationToken cancellationToken)
    {
        var market = await _work.GenericRepository<MarketMessage>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (market == null) throw new NotFoundException("Not Found market");
        return market.ToDto<MarketMessageDto>();
    }
}
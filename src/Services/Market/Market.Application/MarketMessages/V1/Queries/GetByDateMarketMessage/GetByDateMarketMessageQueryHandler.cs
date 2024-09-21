using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.MarketMessageAggregate;

namespace Market.Application.MarketMessages.V1.Queries.GetByDateMarketMessage;

public class GetByDateMarketMessageQueryHandler : IRequestHandler<GetByDateMarketMessageQuery, MarketMessageDto>
{
    private readonly IUnitOfWork _work;

    public GetByDateMarketMessageQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<MarketMessageDto> Handle(GetByDateMarketMessageQuery request,
        CancellationToken cancellationToken)
    {
        var today = DateTime.Now;
        var filter =
            Builders<MarketMessage>.Filter.Where(x => x.StartDate.Date <= today.Date && x.EndDate.Date >= today.Date);
        var result = await _work.GenericRepository<MarketMessage>()
            .GetSingleDocumentByFilterAsync(filter, cancellationToken);
        return result.ToDto<MarketMessageDto>();
    }
}
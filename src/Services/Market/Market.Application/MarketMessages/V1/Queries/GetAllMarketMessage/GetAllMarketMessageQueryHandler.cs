using Market.Application.Common.Interfaces.Persistence.UoW;
using Market.Application.Common.Mapping;
using Market.Application.Dtos;
using Market.Domain.Aggregates.MarketMessageAggregate;

namespace Market.Application.MarketMessages.V1.Queries.GetAllMarketMessage;

public class GetAllMarketMessageQueryHandler:IRequestHandler< GetAllMarketMessageQuery,PaginationResult<MarketMessageDto>>
{
    private readonly IUnitOfWork _work;

    public GetAllMarketMessageQueryHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<PaginationResult<MarketMessageDto>> Handle(GetAllMarketMessageQuery request, CancellationToken cancellationToken)
    {
        var result = await _work.GenericRepository<MarketMessage>()
            .GetAllPaginationAsync(request.Page, request.PageSize, cancellationToken);
        return PaginationResult<MarketMessageDto>.CreatePaginationResult(request.Page, request.PageSize, result.Data.Count,
            result.Data.ToDto<MarketMessageDto>().ToList()); 
        
        
    }
}
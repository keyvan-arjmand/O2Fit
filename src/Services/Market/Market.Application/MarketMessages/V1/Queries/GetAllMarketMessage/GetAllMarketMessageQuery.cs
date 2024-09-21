using Market.Application.Dtos;

namespace Market.Application.MarketMessages.V1.Queries.GetAllMarketMessage;

public record GetAllMarketMessageQuery(int Page, int PageSize):IRequest<PaginationResult<MarketMessageDto>>;
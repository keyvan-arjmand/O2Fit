using Market.Application.Dtos;

namespace Market.Application.MarketMessages.V1.Queries.GetByDateMarketMessage;

public record GetByDateMarketMessageQuery():IRequest<MarketMessageDto>;
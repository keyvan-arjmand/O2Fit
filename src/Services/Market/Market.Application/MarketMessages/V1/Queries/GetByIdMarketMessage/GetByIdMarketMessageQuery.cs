using Market.Application.Dtos;

namespace Market.Application.MarketMessages.V1.Queries.GetByIdMarketMessage;

public record GetByIdMarketMessageQuery(string Id):IRequest<MarketMessageDto>;
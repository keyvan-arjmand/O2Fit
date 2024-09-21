namespace Market.Application.MarketMessages.V1.Commands.SoftDeleteMarketMessage;

public record SoftDeleteMarketMessageCommand(string Id):IRequest;
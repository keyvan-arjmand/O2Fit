namespace Discount.Application.Currencies.V1.Commands.PartialUpdateCurrency;

public record PartialUpdateCurrencyCommand(string CurrencyCode ,double CoefficientCurrency):IRequest;
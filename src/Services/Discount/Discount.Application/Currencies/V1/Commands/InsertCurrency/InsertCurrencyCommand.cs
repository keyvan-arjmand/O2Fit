namespace Discount.Application.Currencies.V1.Commands.InsertCurrency;

public record InsertCurrencyCommand(string CurrencyCode, List<int> CountryIds, double CoefficientCurrency):IRequest;
namespace EventBus.Messages.Events.Services.Discount.Command.DiscountCurrencyRate;

public record UpdateCurrencyRate
    (string Id, List<int> CountryIds, string CurrencyCode, double CoefficientCurrency) : BaseEvent;
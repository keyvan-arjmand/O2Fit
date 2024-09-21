namespace EventBus.Messages.Events.Services.Currency;

public record PartialUpdatedCurrencyCode(string CurrencyCode, List<int> CountryIds) : BaseEvent;
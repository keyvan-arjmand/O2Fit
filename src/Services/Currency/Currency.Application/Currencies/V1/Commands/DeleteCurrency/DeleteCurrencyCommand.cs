namespace Currency.Application.Currencies.V1.Commands.DeleteCurrency;

public class DeleteCurrencyCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
}
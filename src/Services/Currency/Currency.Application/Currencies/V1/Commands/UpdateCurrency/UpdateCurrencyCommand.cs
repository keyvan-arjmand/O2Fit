using Currency.Application.Dtos;
using Currency.Domain.ValueObjects;

namespace Currency.Application.Currencies.V1.Commands.UpdateCurrency;

public class UpdateCurrencyCommand : IRequest<string>
{
    public string Id { get; set; } = string.Empty;
    public double CoefficientCurrency { get; set; } //45932.863669 Riyal => 1 Usd
    public TranslationDto Name { get; set; } = default!;
    public List<int> CountryIds { get; set; } = default!;

}
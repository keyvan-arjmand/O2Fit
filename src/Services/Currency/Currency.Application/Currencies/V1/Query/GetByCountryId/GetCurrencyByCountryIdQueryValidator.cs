using EventBus.Messages.Contracts.Services.Currencies.Currencies;

namespace Currency.Application.Currencies.V1.Query.GetByCountryId;

public class GetCurrencyByCountryIdQueryValidator : AbstractValidator<GetCurrencyByCountryId>
{
    public GetCurrencyByCountryIdQueryValidator()
    {
        RuleFor(x => x.CountryIds).NotNull().WithMessage("Id Can not be null").NotEmpty()
            .WithMessage("Id Can not be empty");
    }
}
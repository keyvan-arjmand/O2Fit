
namespace Currency.Application.Currencies.V1.Query.GetByIdCurrency;

public class GetByIdCurrencyQueryValidator : AbstractValidator<GetByIdCurrencyQuery>
{
    public GetByIdCurrencyQueryValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id Can not be null").NotEmpty()
            .WithMessage("Id Can not be empty");
    }
}
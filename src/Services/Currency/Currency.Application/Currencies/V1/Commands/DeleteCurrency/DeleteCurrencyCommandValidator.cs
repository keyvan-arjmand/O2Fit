namespace Currency.Application.Currencies.V1.Commands.DeleteCurrency;

public class DeleteCurrencyCommandValidator : AbstractValidator<DeleteCurrencyCommand>
{
    public DeleteCurrencyCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id Can not be null").NotEmpty()
            .WithMessage("Id Can not be empty");
    }
}
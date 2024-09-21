namespace Wallet.Application.Wallets.V1.Command.SubtractWallet;

public class SubtractWalletCommandValidator:AbstractValidator<SubtractWalletCommand>
{
    public SubtractWalletCommandValidator()
    {
        RuleFor(x => x.CurrencyCode).NotEmpty().WithMessage("CurrencyName can not be empty").NotNull().WithMessage("CurrencyName can not be Null");
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be Null");
        RuleFor(x => x.SubtractAmount).GreaterThan(0).WithMessage("Amount Most Greater than 0").NotEmpty().WithMessage("Currency can not be empty").NotNull().WithMessage("Currency can not be Null");
    }
}

namespace Wallet.Application.Wallets.V1.Command.WalletCharge;

public class WalletChargeCommandValidator : AbstractValidator<WalletChargeCommand>
{
    public WalletChargeCommandValidator()
    {
        RuleFor(x => x.CurrencyCode).NotEmpty().WithMessage("Currency can not be empty").NotNull().WithMessage("Currency can not be Null");
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be Null");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount Most Greater than 0").NotEmpty().WithMessage("Currency can not be empty").NotNull().WithMessage("Currency can not be Null");
    }
}
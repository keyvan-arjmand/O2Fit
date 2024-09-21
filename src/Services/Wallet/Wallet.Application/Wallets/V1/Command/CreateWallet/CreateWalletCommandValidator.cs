namespace Wallet.Application.Wallets.V1.Command.CreateWallet;

public class CreateWalletCommandValidator : AbstractValidator<CreateWalletCommand>
{
    public CreateWalletCommandValidator()
    {
        RuleFor(x => x.CurrencyCode).NotEmpty().WithMessage("Currency can not be empty").NotNull().WithMessage("Currency can not be Null");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId can not be empty").NotNull().WithMessage("UserId can not be Null");
    }
}
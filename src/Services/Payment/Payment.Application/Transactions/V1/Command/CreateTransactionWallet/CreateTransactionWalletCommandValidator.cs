using Common.Enums.TypeEnums;

namespace Payment.Application.Transactions.V1.Command.CreateTransactionWallet;

public class CreateTransactionWalletCommandValidator:AbstractValidator<CreateTransactionWalletCommand>
{
    public CreateTransactionWalletCommandValidator()
    {
        RuleFor(x => x.AmountCharge).GreaterThan(0).WithMessage("Amount Charge Not valid").NotEmpty().WithMessage("PackageId Can not be Empty").NotNull().WithMessage("PackageId Can not be Null");
        RuleFor(x => x.CurrencyCode).NotEmpty().WithMessage("CurrencyName can not be empty").NotNull().WithMessage("CurrencyName can not be Null");
        RuleFor(x => x.Bank).IsInEnum();
        RuleFor(x => x.UserType).IsInEnum();
    }
}
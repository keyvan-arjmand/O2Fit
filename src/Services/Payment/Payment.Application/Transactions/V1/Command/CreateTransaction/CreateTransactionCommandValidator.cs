using Common.Enums.TypeEnums;

namespace Payment.Application.Transactions.V1.Command.CreateTransaction;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.PackageId).NotEmpty().WithMessage("PackageId Can not be Empty").NotNull().WithMessage("PackageId Can not be Null");
        RuleFor(x => x.Bank).IsInEnum();
        RuleFor(x => x.UserType).IsInEnum();
    }
}
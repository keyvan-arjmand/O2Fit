namespace Payment.Application.Transactions.V1.Command.UpdateTransactionSaman;

public class UpdateTransactionSamanCommandValidator:AbstractValidator<UpdateTransactionSamanCommand>
{
    public UpdateTransactionSamanCommandValidator()
    {
        RuleFor(x => x.SamanResult).NotEmpty().WithMessage("PackageId Can not be Empty").NotNull().WithMessage("PackageId Can not be Null");
        RuleFor(x => x.TransactionId).NotEmpty().WithMessage("DiscountCode can not be empty").NotNull().WithMessage("DiscountCode can not be Null");
        RuleFor(x => x.Status).IsInEnum().NotEmpty().WithMessage("Status can not be empty").NotNull().WithMessage("Status can not be Null");
    }
}
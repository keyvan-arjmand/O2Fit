namespace Payment.Application.Transactions.V1.Command.UpdateTransactionCafeBazar;

public class UpdateTransactionCafeBazarCommandValidator:AbstractValidator<UpdateTransactionCafeBazarCommand>
{

    public UpdateTransactionCafeBazarCommandValidator()
    {
        RuleFor(x => x.SaleReferenceId).NotEmpty().WithMessage("SaleReferenceId Can not be Empty").NotNull().WithMessage("SaleReferenceId Can not be Null");
        RuleFor(x => x.TransactionId).NotEmpty().WithMessage("TransactionId can not be empty").NotNull().WithMessage("TransactionId can not be Null");
        RuleFor(x => x.IsSuccess).NotEmpty().WithMessage("IsSuccess can not be empty").NotNull().WithMessage("IsSuccess can not be Null");
    }
}
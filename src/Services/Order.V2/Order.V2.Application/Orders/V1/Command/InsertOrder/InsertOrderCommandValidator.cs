namespace Order.V2.Application.Orders.V1.Command.InsertOrder;

public class InsertOrderCommandValidator:AbstractValidator<InsertOrderCommand>
{
    public InsertOrderCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull().WithMessage("Id Can not be null").NotEmpty()
            .WithMessage("Id Can not be empty");
        RuleFor(x => x.PaymentTransactionId).NotNull().WithMessage("PaymentTransactionId Can not be null").NotEmpty()
            .WithMessage("PaymentTransactionId Can not be empty");
        RuleFor(x => x.WalletTransactionId).NotNull().WithMessage("WalletTransactionId Can not be null").NotEmpty()
            .WithMessage("WalletTransactionId Can not be empty");
    }
}
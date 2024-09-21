namespace Payment.Application.Transactions.V1.Query.GetByBankOrderId;

public class GetByBankOrderIdQueryValidator:AbstractValidator<GetByBankOrderIdQuery >
{
    public GetByBankOrderIdQueryValidator()
    {
        RuleFor(x => x.BankOrderId).GreaterThan(0).WithMessage("BankOrderId Note Valid").NotEmpty().WithMessage("BankOrderId Can not be Empty").NotNull().WithMessage("BankOrderId Can not be Null");
     
    }
}
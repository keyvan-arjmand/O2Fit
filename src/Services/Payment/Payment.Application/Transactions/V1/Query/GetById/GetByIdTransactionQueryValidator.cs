namespace Payment.Application.Transactions.V1.Query.GetById;

public class GetByIdTransactionQueryValidator:AbstractValidator<GetByIdTransactionQuery>
{
    public GetByIdTransactionQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id Can not be Empty").NotNull().WithMessage("Id Can not be Null");

    }
}
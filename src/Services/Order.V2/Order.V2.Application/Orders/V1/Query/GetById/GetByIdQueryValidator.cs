namespace Order.V2.Application.Orders.V1.Query.GetById;

public class GetByIdQueryValidator:AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id Can not be Empty").NotNull().WithMessage("Id Can not be Null");
    }
}
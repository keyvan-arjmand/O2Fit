namespace Food.V2.Application.Foods.V1.Queries.GetFoodNameById;

public class GetFoodNameByIdQueryValidator : AbstractValidator<GetFoodNameByIdQuery>
{
    public GetFoodNameByIdQueryValidator()
    {
        RuleFor(x=>x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");
    }
}
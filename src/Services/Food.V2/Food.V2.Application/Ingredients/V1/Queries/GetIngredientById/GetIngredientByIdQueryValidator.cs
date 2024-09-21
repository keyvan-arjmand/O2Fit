namespace Food.V2.Application.Ingredients.V1.Queries.GetIngredientById;

public class GetIngredientByIdQueryValidator : AbstractValidator<GetIngredientByIdQuery>
{
    public GetIngredientByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");
    }
}
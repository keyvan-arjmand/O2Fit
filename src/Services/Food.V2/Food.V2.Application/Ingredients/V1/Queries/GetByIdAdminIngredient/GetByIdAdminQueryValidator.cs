namespace Food.V2.Application.Ingredients.V1.Queries.GetByIdAdminIngredient;

public class GetByIdAdminIngredientQueryValidator : AbstractValidator<GetByIdAdminIngredientQuery>
{
    public GetByIdAdminIngredientQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty")
            .NotNull().WithMessage("Id can not be null");
    }
}
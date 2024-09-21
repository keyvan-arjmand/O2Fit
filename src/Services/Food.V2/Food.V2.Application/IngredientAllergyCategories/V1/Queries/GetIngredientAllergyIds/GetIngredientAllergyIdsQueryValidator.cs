namespace Food.V2.Application.IngredientAllergyCategories.V1.Queries.GetIngredientAllergyIds;

public class GetIngredientAllergyIdsQueryValidator : AbstractValidator<GetIngredientAllergyIdsQuery>
{
    public GetIngredientAllergyIdsQueryValidator()
    {
        RuleFor(x=>x.IngredientId).NotEmpty().WithMessage("IngredientId can not be empty")
            .NotNull().WithMessage("IngredientId can not be null");
    }
}
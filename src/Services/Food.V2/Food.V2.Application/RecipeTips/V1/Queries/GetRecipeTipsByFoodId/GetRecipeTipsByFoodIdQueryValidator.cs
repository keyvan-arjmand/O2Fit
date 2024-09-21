namespace Food.V2.Application.RecipeTips.V1.Queries.GetRecipeTipsByFoodId;

public class GetRecipeTipsByFoodIdQueryValidator : AbstractValidator<GetRecipeTipsByFoodIdQuery>
{
    public GetRecipeTipsByFoodIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("id cannot null or empty");
    }
}
namespace Food.V2.Application.RecipeTips.V1.Queries.GetByIdRecipeTip;

public class GetByIdRecipeTipQueryValidator:AbstractValidator<GetByIdRecipeTipQuery>
{
    public GetByIdRecipeTipQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("id cannot null or empty");
    }
}
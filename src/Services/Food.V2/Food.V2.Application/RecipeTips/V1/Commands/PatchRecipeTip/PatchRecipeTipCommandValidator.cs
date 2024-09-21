namespace Food.V2.Application.RecipeTips.V1.Commands.PatchRecipeTip;

public class PatchRecipeTipCommandValidator : AbstractValidator<PatchRecipeTipCommand>
{
    public PatchRecipeTipCommandValidator()
    {
        RuleFor(x => x.FoodId).NotEmpty().NotNull().WithMessage("FoodId cannot null or empty");
        RuleFor(x => x.Tips).Must(x => x.Count > 0).NotEmpty().NotNull().WithMessage("Tips cannot null or empty");
    }
}
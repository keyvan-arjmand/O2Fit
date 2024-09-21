namespace Food.V2.Application.RecipeTips.V1.Commands.CreateRecipeTips;

public class CreateRecipeTipsCommandValidator : AbstractValidator<CreateRecipeTipsCommand>
{
    public CreateRecipeTipsCommandValidator()
    {
        RuleFor(x => x.FoodId).NotEmpty().NotNull().WithMessage("FoodId cannot null or empty");
        RuleFor(x => x.Tips).NotEmpty().NotNull().Must(x => x.Count > 0).WithMessage("Steps cannot null or empty");
    }
}
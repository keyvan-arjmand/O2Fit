namespace Food.V2.Application.RecipeTips.V1.Commands.SoftDeleteRecipeTip;

public class SoftDeleteRecipeTipCommandValidator:AbstractValidator<SoftDeleteRecipeTipCommand>
{
    public SoftDeleteRecipeTipCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id cannot null or empty");

    }
}
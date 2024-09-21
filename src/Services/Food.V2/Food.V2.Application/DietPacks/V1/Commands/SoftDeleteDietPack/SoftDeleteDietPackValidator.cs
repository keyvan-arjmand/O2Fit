namespace Food.V2.Application.DietPacks.V1.Commands.SoftDeleteDietPack;

public class SoftDeleteDietPackValidator : AbstractValidator<SoftDeleteDietPackCommand>
{
    public SoftDeleteDietPackValidator()
    {
        RuleFor(x=>x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull()
            .WithMessage("Id can not be null");
    }
}
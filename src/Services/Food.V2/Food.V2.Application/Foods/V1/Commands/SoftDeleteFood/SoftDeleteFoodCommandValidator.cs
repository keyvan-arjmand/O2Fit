namespace Food.V2.Application.Foods.V1.Commands.SoftDeleteFood;

public class SoftDeleteFoodCommandValidator : AbstractValidator<SoftDeleteFoodCommand>
{
    public SoftDeleteFoodCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");

    }
}
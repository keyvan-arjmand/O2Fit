namespace Payment.Application.PackagesNutritionist.V1.Commands.SoftDeletePackageNutritionist;

public class DeletePackageNutritionistCommandValidator : AbstractValidator<DeletePackageNutritionistCommand>
{
    public DeletePackageNutritionistCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be Empty").NotNull().WithMessage("Id can not be Null");
    }
}
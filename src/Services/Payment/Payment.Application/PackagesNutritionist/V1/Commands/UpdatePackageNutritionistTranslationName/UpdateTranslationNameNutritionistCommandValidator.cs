namespace Payment.Application.PackagesNutritionist.V1.Commands.UpdatePackageNutritionistTranslationName;

public class UpdateTranslationDescriptionCommandValidator : AbstractValidator<UpdateTranslationNameNutritionistCommand>
{
    public UpdateTranslationDescriptionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id Can Not Be Empty").NotNull().WithMessage("Id Can Not Be Null");
    }
}
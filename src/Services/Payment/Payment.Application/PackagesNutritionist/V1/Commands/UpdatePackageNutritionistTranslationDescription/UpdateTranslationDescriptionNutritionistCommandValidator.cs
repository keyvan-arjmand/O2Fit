using Payment.Application.PackagesNutritionist.V1.Commands.UpdatePackageNutritionistTranslationName;

namespace Payment.Application.PackagesNutritionist.V1.Commands.UpdatePackageNutritionistTranslationDescription;

public class UpdateTranslationDescriptionNutritionistCommandValidator : AbstractValidator<UpdateTranslationNameNutritionistCommand>
{
    public UpdateTranslationDescriptionNutritionistCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id Can Not Be Empty").NotNull().WithMessage("Id Can Not Be Null");
    }
}
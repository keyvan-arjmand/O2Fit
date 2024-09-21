namespace Payment.Application.PackagesNutritionist.V1.Commands.CreatePackageNutritionist;

public class CreatePackageNutritionistCommandValidator : AbstractValidator<CreatePackageNutritionistCommand>
{
    public CreatePackageNutritionistCommandValidator()
    {
        RuleFor(x => x.Description).NotNull().WithMessage("Description can not be Null");
        RuleFor(x => x.Name).NotNull().WithMessage("Name can not be Null");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).NotEmpty().WithMessage("Price can not be Empty").NotNull().WithMessage("Price can not be Null");
        RuleFor(x => x.Sort).GreaterThanOrEqualTo(0).NotEmpty().WithMessage("Sort can not be Empty").NotNull().WithMessage("Sort can not be Null");
    }
}
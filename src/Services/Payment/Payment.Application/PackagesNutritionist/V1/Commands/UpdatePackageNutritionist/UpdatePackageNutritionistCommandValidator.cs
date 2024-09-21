namespace Payment.Application.PackagesNutritionist.V1.Commands.UpdatePackageNutritionist;

public class UpdatePackageNutritionistCommandValidator : AbstractValidator<UpdatePackageNutritionistCommand>
{
    public UpdatePackageNutritionistCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be Empty").NotNull().WithMessage("Id can not be Null");
        RuleFor(x => x.Description).NotNull().WithMessage("Description can not be Null");
        RuleFor(x => x.Name).NotNull().WithMessage("Name can not be Null");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price can not be Empty").NotNull().WithMessage("Price can not be Null");
        RuleFor(x => x.CurrencyCode).IsInEnum().NotNull().WithMessage("Currency can not be Null");
        RuleFor(x => x.Sort).GreaterThanOrEqualTo(0).NotEmpty().WithMessage("Sort can not be Empty").NotNull().WithMessage("Sort can not be Null");
    }
}
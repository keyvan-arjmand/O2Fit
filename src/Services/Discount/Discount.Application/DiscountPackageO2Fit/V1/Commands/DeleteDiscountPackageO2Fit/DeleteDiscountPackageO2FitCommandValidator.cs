namespace Discount.Application.DiscountPackageO2Fit.V1.Commands.DeleteDiscountPackageO2Fit;

public class DeleteDiscountPackageO2FitCommandValidator : AbstractValidator<DeleteDiscountPackageO2FitCommand>
{

    public DeleteDiscountPackageO2FitCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}

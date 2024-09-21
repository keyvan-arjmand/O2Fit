namespace Discount.Application.DiscountPackageO2Fit.V1.Commands.UpdateDiscountPackageO2Fit;

public class UpdateDiscountPackageO2FitCommandValidator : AbstractValidator<UpdateDiscountPackageO2FitCommand>
{
    public UpdateDiscountPackageO2FitCommandValidator()
    {
        RuleFor(x => x.Percent).NotEmpty().WithMessage("Percent can not be empty").NotNull().WithMessage("Percent can not be Null")
            .GreaterThan(0).WithMessage("Percent Not Valid").LessThanOrEqualTo(100).WithMessage("Percent Not Valid"); ;
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be Null");

    }
}
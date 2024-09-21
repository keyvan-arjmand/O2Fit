namespace Discount.Application.DiscountPackageO2Fit.V1.Query.GetByIdDiscountPackageO2Fit;

public class GetByIdDiscountPackageO2FitQueryValidator:AbstractValidator<GetByIdDiscountPackageO2FitQuery>
{
    public GetByIdDiscountPackageO2FitQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id Can not be empty").NotNull().WithMessage("Id Can not be null");
    }
}
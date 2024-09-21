namespace Payment.Application.PackagesNutritionist.V1.Query.PackageNutritionistById;

public class GetPackageNutritionistByIdQueryValidator : AbstractValidator<GetPackageNutritionistByIdQuery>
{
    public GetPackageNutritionistByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be Empty").NotNull().WithMessage("Id can not be Null");
    }
}
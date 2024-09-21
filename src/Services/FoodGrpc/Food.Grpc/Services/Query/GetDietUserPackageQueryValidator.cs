using FluentValidation;

namespace Food.Grpc.Services.Query;

public class GetDietUserPackageQueryValidator : AbstractValidator<GetDietUserPackageQuery>
{
    public GetDietUserPackageQueryValidator()
    {
        RuleFor(x => x.DietPacks.DietCategoryId).NotEmpty().WithMessage("DietCategoryId Cannot be Empty")
            .NotNull().WithMessage("DietCategoryId Cannot be Null");
        RuleFor(x => x.DietPacks.DailyCalorie).NotEmpty().WithMessage("DailyCalorie Cannot be Empty")
            .NotNull().WithMessage("DailyCalorie Cannot be Null");
    }
}
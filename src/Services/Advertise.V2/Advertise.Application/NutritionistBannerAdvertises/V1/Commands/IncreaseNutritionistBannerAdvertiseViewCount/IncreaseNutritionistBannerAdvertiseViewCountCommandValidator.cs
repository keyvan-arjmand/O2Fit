namespace Advertise.Application.NutritionistBannerAdvertises.V1.Commands.IncreaseNutritionistBannerAdvertiseViewCount;

public class IncreaseNutritionistBannerAdvertiseViewCountCommandValidator : AbstractValidator<IncreaseNutritionistBannerAdvertiseViewCountCommand>
{
    public IncreaseNutritionistBannerAdvertiseViewCountCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}
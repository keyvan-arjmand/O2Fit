namespace Advertise.Application.NutritionistBannerAdvertises.V1.Commands.IncreaseNutritionistBannerAdvertiseClickCount;

public class IncreaseNutritionistBannerAdvertiseClickCountCommandValidator : AbstractValidator<IncreaseNutritionistBannerAdvertiseClickCountCommand>
{
    public IncreaseNutritionistBannerAdvertiseClickCountCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}
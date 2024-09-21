namespace Advertise.Application.NutritionistBannerAdvertises.V1.Commands.DecreaseNutritionistBannerAdvertiseClickCount;

public class DecreaseNutritionistBannerAdvertiseClickCountCommandValidator : AbstractValidator<DecreaseNutritionistBannerAdvertiseClickCountCommand>
{
    public DecreaseNutritionistBannerAdvertiseClickCountCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}
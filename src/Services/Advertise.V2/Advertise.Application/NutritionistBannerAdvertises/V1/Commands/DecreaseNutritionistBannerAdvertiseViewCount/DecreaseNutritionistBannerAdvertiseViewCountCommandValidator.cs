namespace Advertise.Application.NutritionistBannerAdvertises.V1.Commands.DecreaseNutritionistBannerAdvertiseViewCount;

public class DecreaseNutritionistBannerAdvertiseViewCountCommandValidator : AbstractValidator<DecreaseNutritionistBannerAdvertiseViewCountCommand>
{
    public DecreaseNutritionistBannerAdvertiseViewCountCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}
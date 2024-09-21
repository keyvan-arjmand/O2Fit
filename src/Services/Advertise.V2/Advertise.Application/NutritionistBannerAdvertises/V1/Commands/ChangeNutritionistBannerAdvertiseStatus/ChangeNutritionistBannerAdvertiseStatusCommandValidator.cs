namespace Advertise.Application.NutritionistBannerAdvertises.V1.Commands.ChangeNutritionistBannerAdvertiseStatus;

public class ChangeNutritionistBannerAdvertiseStatusCommandValidator : AbstractValidator<ChangeNutritionistBannerAdvertiseStatusCommand>
{
    public ChangeNutritionistBannerAdvertiseStatusCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
        RuleFor(x => x.Status).IsInEnum();
    }
}
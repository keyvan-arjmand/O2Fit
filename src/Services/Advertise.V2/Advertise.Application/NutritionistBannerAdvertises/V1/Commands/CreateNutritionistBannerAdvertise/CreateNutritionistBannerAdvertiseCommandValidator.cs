namespace Advertise.Application.NutritionistBannerAdvertises.V1.Commands.CreateNutritionistBannerAdvertise;

public class CreateNutritionistBannerAdvertiseCommandValidator : AbstractValidator<CreateNutritionistBannerAdvertiseCommand>
{
    public CreateNutritionistBannerAdvertiseCommandValidator()
    {
        RuleFor(x => x.Budget).GreaterThanOrEqualTo(1).WithMessage("Budget greater than or equal to 1");
        RuleFor(x=>x.Cost).GreaterThanOrEqualTo(1).WithMessage("Cost greater than or equal to 1");
        RuleFor(x => x.Link).NotEmpty().WithMessage("Link can not be empty").NotNull()
            .WithMessage("Link can not be null");
        RuleForEach(x => x.Languages).IsInEnum();
    }
}
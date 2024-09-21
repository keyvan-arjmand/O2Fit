namespace Advertise.Application.NutritionistBannerAdvertises.V1.Queries.GetNutritionistAdvertiseBannerById;

public class GetNutritionistAdvertiseBannerByIdQueryValidator : AbstractValidator<GetNutritionistAdvertiseBannerByIdQuery>
{
    public GetNutritionistAdvertiseBannerByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty").NotNull().WithMessage("Id can not be null");
    }
}
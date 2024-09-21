namespace Track.Application.FavoriteFoods.V1.Queries.GetFavoriteFoodByUserId;

public class GetFavoriteFoodByUserIdQueryValidator:AbstractValidator<GetFavoriteFoodByUserIdQuery>
{

    public GetFavoriteFoodByUserIdQueryValidator()
    {
        RuleFor(x => x.PageSize).GreaterThan(0).NotEmpty().NotNull().WithMessage("PageSize cannot null or empty");
        RuleFor(x => x.Page).GreaterThan(0).NotEmpty().NotNull().WithMessage("Page cannot null or empty");
        RuleFor(x => x.UserId).NotEmpty().NotNull().WithMessage("UserId cannot null or empty");

    }
}
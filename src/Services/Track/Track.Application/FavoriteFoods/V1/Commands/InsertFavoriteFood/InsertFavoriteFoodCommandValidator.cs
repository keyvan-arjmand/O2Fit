namespace Track.Application.FavoriteFoods.V1.Commands.InsertFavoriteFood;

public class InsertFavoriteFoodCommandValidator:AbstractValidator<InsertFavoriteFoodCommand>
{

    public InsertFavoriteFoodCommandValidator()
    {
        RuleFor(x => x.AppId).NotEmpty().NotNull().WithMessage("AppId cannot null or empty");
    }
}
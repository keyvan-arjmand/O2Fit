namespace Track.Application.FavoriteMeals.V1.Commands.SoftDeleteFavoriteMeal;

public record SoftDeleteFavoriteMealCommand(string Id) : IRequest;

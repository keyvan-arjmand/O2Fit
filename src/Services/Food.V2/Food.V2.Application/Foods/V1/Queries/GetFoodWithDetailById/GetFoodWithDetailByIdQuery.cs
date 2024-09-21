using Food.V2.Application.Dtos.Recipe;

namespace Food.V2.Application.Foods.V1.Queries.GetFoodWithDetailById;

public record GetFoodWithDetailByIdQuery(string Id) : IRequest<FullRecipeDto>;

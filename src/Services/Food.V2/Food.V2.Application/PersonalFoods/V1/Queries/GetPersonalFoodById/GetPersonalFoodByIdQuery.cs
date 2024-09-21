using Food.V2.Application.Dtos.PersonalFood;

namespace Food.V2.Application.PersonalFoods.V1.Queries.GetPersonalFoodById;

public record GetPersonalFoodByIdQuery(string Id):IRequest<PersonalFoodDto>;
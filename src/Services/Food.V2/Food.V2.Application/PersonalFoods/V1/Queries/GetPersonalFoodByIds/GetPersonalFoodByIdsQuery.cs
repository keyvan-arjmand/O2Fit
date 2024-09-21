using Food.V2.Application.Dtos.PersonalFood;

namespace Food.V2.Application.PersonalFoods.V1.Queries.GetPersonalFoodByIds;

public record GetPersonalFoodByIdsQuery(List<string> PersonalFoodIds):IRequest<List<PersonalFoodDto>>;
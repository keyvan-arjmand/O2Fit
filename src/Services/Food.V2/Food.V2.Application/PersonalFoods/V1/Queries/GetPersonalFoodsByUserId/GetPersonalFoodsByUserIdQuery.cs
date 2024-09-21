using Food.V2.Application.Dtos.PersonalFood;

namespace Food.V2.Application.PersonalFoods.V1.Queries.GetPersonalFoodsByUserId;

public class GetPersonalFoodsByUserIdQuery : IRequest<List<PersonalFoodDto>>
{
    public string UserId { get; set; } = string.Empty;
}
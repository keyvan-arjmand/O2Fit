namespace Identity.V2.Application.NutritionistProfiles.V1.Queries.GetNutritionistProfileById;

public record GetNutritionistProfileByIdQuery(string UserId) : IRequest<NutritionistDataDto>;
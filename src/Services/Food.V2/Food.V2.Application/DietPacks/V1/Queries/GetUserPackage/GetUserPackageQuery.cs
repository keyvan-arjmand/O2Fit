namespace Food.V2.Application.DietPacks.V1.Queries.GetUserPackage;

public record GetUserPackageQuery(string DietCategoryId, int DailyCalorie, string? AllergyIds) : IRequest<List<GetUserPackageDto>>;
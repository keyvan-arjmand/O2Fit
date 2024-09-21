using Payment.Application.Dtos.DietNutritionist;

namespace Payment.Application.PackageDietNutritionists.V1.Queries.GetByIdPackageDietNutritionist;

public record GetByIdPackageDietNutritionistQuery(string Id, string CurrencyCode) : IRequest<DietNutritionistDto>;
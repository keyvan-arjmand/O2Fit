using Discount.Application.Dtos;

namespace Discount.Application.DiscountPackagesNutritionist.V1.Query.GetByIdDiscountPackageNutritionist;

public record GetByIdDiscountNutritionistPackageQuery(string Id) : IRequest<DiscountPackageDto>;

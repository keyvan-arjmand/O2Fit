using Discount.Application.Dtos;

namespace Discount.Application.DiscountPackagesNutritionist.V1.Query.GetAllDiscountPackageNutritionist;

public record GetAllDiscountPackageNutritionistQuery : IRequest<List<DiscountPackageDto>>;

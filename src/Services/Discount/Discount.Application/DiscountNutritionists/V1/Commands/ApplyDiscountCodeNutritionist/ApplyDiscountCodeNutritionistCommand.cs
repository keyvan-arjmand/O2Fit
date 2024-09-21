using Discount.Application.Dtos;

namespace Discount.Application.DiscountNutritionists.V1.Commands.ApplyDiscountCodeNutritionist;

public record ApplyDiscountCodeNutritionistCommand(string DiscountCode, string PackageId, int UserCountryId,
    string UserId, string UserCurrencyCode) : IRequest<ApplyDiscountCodeNutritionistResult>;
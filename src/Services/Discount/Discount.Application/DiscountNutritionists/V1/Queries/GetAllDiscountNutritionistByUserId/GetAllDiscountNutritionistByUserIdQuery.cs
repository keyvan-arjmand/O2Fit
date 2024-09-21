using Discount.Application.Dtos;

namespace Discount.Application.DiscountNutritionists.V1.Queries.GetAllDiscountNutritionistByUserId;

public record GetAllDiscountNutritionistByUserIdQuery(string UserId):IRequest<List<DiscountO2FitDto>>;
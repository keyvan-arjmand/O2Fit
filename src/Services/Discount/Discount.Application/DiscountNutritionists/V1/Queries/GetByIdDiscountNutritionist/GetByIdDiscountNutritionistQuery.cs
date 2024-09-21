using Discount.Application.Dtos;

namespace Discount.Application.DiscountNutritionists.V1.Queries.GetByIdDiscountNutritionist;

public record GetByIdDiscountNutritionistQuery(string Id):IRequest<DiscountO2FitDto>;
using Discount.Application.Dtos;

namespace Discount.Application.DiscountO2Fits.V1.Queries.GetAllByUserId;

public record GetAllDiscountByUserIdQuery(string UserId):IRequest<List<DiscountO2FitDto>>;